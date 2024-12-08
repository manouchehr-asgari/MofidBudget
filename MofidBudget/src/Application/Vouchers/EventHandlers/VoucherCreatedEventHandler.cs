using System.Globalization;
using MediatR;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Costs.Commands.CreateCost;
using MofidBudget.Application.Vouchers.Commands.CreateVoucher;
using MofidBudget.Domain.Entities;
using MofidBudget.Domain.Enums;
using MofidBudget.Domain.Events;

namespace MofidBudget.Application.Vouchers.EventHandlers;

public class VoucherCreatedEventHandler : INotificationHandler<VoucherCreatedEvent>
{

    private readonly IApplicationDbContext _context;
    private readonly ISender _sender;
    public VoucherCreatedEventHandler(IApplicationDbContext context, ISender sender)
    {
        _context = context;
        _sender = sender;
    }

    public async Task Handle(VoucherCreatedEvent notification, CancellationToken cancellationToken)
    {
        CreateCostCommand command = new CreateCostCommand
        {
            Amount = notification.Item.Amount,
            RefractionLevel = RefractionLevel.Direct,
            BeneficiaryId = notification.Item.BeneficiaryId,
            CostTypeId = notification.Item.CostTypeId,
            FromBeneficiaryId = null,
            VoucherDate = notification.Item.VoucherDate,
            VoucherId = notification.Item.Id
        };
        _ = _sender.Send(command);
        await CalculateCostDetails(notification.Item.BeneficiaryId, notification.Item.Amount, notification);

    }
    public async Task CalculateCostDetails(int beneficiaryId, decimal cost, VoucherCreatedEvent notification)
    {
        var relations = _context.BeneficiaryRelationShips.Where(q => q.FromBeneficiaryId == beneficiaryId);
        if (relations.Any())
        {
            var RefractionRelations = relations.Where(q => q.RefractionType == RefractionType.Location).ToList();
            if (RefractionRelations.Any())
            {
                await CalculateRefractionLocationCostDetails(RefractionRelations, cost, notification);
            }
            else
            {
                var BeneficiaryRelations = relations.Where(q => q.RefractionType == RefractionType.Beneficiary).ToList();
                foreach (var relation in BeneficiaryRelations)
                {
                    await CalculateBeneficiaryCostDetails(relation, cost, notification);
                }
            }
        }
    }
    public async Task CalculateBeneficiaryCostDetails(BeneficiaryRelationShip relation, decimal cost, VoucherCreatedEvent notification)
    {

        //var relations = await _context.BeneficiaryRelationShips.FirstOrDefaultAsync(q => q.FromBeneficiaryId == beneficiaryId);
        // if (relations != null)
        //  {
        var amount = cost * relation.Percent;
        CreateCostCommand command = new CreateCostCommand
        {
            Amount = amount ?? 0,
            RefractionLevel = relation.RefractionLevel,
            BeneficiaryId = relation.ToBeneficiaryId ?? 0,
            CostTypeId = notification.Item.CostTypeId,
            FromBeneficiaryId = relation.FromBeneficiaryId,
            VoucherDate = notification.Item.VoucherDate,
            VoucherId = notification.Item.Id
        };
        _ = _sender.Send(command);
        await CalculateCostDetails(relation.ToBeneficiaryId ?? 0, amount ?? 0, notification);
        // }
    }
    public async Task CalculateRefractionLocationCostDetails(List<BeneficiaryRelationShip> relations, decimal cost, VoucherCreatedEvent notification)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            var voucherDate = notification.Item.VoucherDate ?? DateTime.Now;
            var firstDayOfMonth = pc.ToDateTime(pc.GetYear(voucherDate), pc.GetMonth(voucherDate), 1, 0, 0, 0, 0);
            
            var relationLocations = relations.Select(p => p.ToLocation).ToList();
            var beneficiaryLocations = _context.Beneficiaries.Where(q => relationLocations.Contains(q.Location)).Select(p => p.Id).ToList();

            if (relations.Any(q=>q.RefractionLevel==RefractionLevel.EnablingTeam)) {
                var excludedBeneficiarie = _context.BeneficiaryRelationShips.Where(q => q.RefractionLevel==RefractionLevel.EnablingTeam||q.RefractionLevel == RefractionLevel.Enabling).Select(p => p.FromBeneficiaryId).ToList();
                beneficiaryLocations = beneficiaryLocations.Where(q => !excludedBeneficiarie.Contains(q)).ToList();
            }
            
            
            
            var beneficiaryEmplyees = _context.BeneficiaryEmplyees.Where(q => q.Date >= firstDayOfMonth && q.Date <= firstDayOfMonth.AddDays(29) && beneficiaryLocations.Contains( q.BeneficiaryId)).ToList();

            var allEmployeeCount = beneficiaryEmplyees.Sum(q => q.EmployeeCount);



            foreach (var beneficiary in beneficiaryEmplyees)
            {
                var amount = (cost * beneficiary.EmployeeCount) / allEmployeeCount;
                CreateCostCommand command = new CreateCostCommand
                {
                    Amount = amount,
                    RefractionLevel = relations.First().RefractionLevel,
                    BeneficiaryId = beneficiary.BeneficiaryId,
                    CostTypeId = notification.Item.CostTypeId,
                    FromBeneficiaryId = relations.First().FromBeneficiaryId,
                    VoucherDate = notification.Item.VoucherDate,
                    VoucherId = notification.Item.Id
                };
                _ = _sender.Send(command);
                await CalculateCostDetails(beneficiary.BeneficiaryId, amount, notification);
            }
        }
        catch (Exception x)
        {
            var str=x.GetBaseException();
            throw;
        }
       



    }

}
