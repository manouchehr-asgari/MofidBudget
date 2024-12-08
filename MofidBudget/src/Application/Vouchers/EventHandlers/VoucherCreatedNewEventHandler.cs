using System.Globalization;
using MediatR;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Costs.Commands.CreateCost;
using MofidBudget.Application.Vouchers.Commands.CreateVoucher;
using MofidBudget.Domain.Entities;
using MofidBudget.Domain.Enums;
using MofidBudget.Domain.Events;

namespace MofidBudget.Application.Vouchers.EventHandlers
{
    public class VoucherCreatedNewEventHandler : INotificationHandler<VoucherCreatedEvent>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISender _sender;

        public VoucherCreatedNewEventHandler(IApplicationDbContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
        }

        public async Task Handle(VoucherCreatedEvent notification, CancellationToken cancellationToken)
        {
            var initialCommand = new CreateCostCommand
            {
                Amount = (decimal)notification.Item.Amount,
                RefractionLevel = RefractionLevel.Direct,
                BeneficiaryId = notification.Item.BeneficiaryId,
                CostTypeId = notification.Item.CostTypeId,
                FromBeneficiaryId = null,
                VoucherDate = notification.Item.VoucherDate,
                VoucherId = notification.Item.Id
            };

            await _sender.Send(initialCommand, cancellationToken);
            await CalculateCostDetails(notification.Item.BeneficiaryId, (decimal)notification.Item.Amount, notification, cancellationToken);
        }

        private async Task CalculateCostDetails(int beneficiaryId, decimal cost, VoucherCreatedEvent notification, CancellationToken cancellationToken)
        {
            var queue = new Queue<(int BeneficiaryId, decimal Cost)>();
            queue.Enqueue((beneficiaryId, cost));

            var allRelations = _context.BeneficiaryRelationShips.Where(q=>q.FromDate<=notification.Item.VoucherDate&& q.ToDate >= notification.Item.VoucherDate).ToList();
            var allBeneficiaries = _context.Beneficiaries.ToList();
            var allBeneficiaryEmployees = _context.BeneficiaryEmplyees.ToList();

            var batchCommands = new List<CreateCostCommand>();

            while (queue.Count > 0)
            {
                var (currentBeneficiaryId, currentCost) = queue.Dequeue();
                var relations = allRelations.Where(q => q.FromBeneficiaryId == currentBeneficiaryId).ToList();

                if (!relations.Any()) continue;

                var refractionRelations = relations.Where(q => q.RefractionType == RefractionType.Location).ToList();
                if (refractionRelations.Any())
                {
                     CalculateRefractionLocationCostDetails(refractionRelations, currentCost, notification, queue, allRelations, allBeneficiaries, allBeneficiaryEmployees, batchCommands, cancellationToken);
                }
                else
                {
                    var beneficiaryRelations = relations.Where(q => q.RefractionType == RefractionType.Beneficiary).ToList();
                    foreach (var relation in beneficiaryRelations)
                    {
                        var amount = currentCost * relation.Percent ?? 0;
                        var command = new CreateCostCommand
                        {
                            Amount = amount,
                            RefractionLevel = relation.RefractionLevel,
                            BeneficiaryId = relation.ToBeneficiaryId ?? 0,
                            CostTypeId = notification.Item.CostTypeId,
                            FromBeneficiaryId = relation.FromBeneficiaryId,
                            VoucherDate = notification.Item.VoucherDate,
                            VoucherId = notification.Item.Id
                        };

                        batchCommands.Add(command);
                        queue.Enqueue((relation.ToBeneficiaryId ?? 0, amount));
                    }

                    var bussinessLineRelations = relations.Where(q => q.RefractionType == RefractionType.Bussiness).ToList();
                    foreach (var relation in bussinessLineRelations)
                    {
                        var amount = currentCost * relation.Percent ?? 0;
                        var command = new CreateCostCommand
                        {
                            Amount = amount,
                            RefractionLevel = relation.RefractionLevel,
                            BussinessLine = relation.ToBussinessLine,
                            CostTypeId = notification.Item.CostTypeId,
                            FromBeneficiaryId = relation.FromBeneficiaryId,
                            VoucherDate = notification.Item.VoucherDate,
                            VoucherId = notification.Item.Id
                        };

                        batchCommands.Add(command);
                        queue.Enqueue((relation.ToBeneficiaryId ?? 0, amount));
                    }
                }
            }

            if (batchCommands.Any())
            {
                var batchCommand = new CreateCostWithBatchCommand { Requests = batchCommands };
                await _sender.Send(batchCommand, cancellationToken);
            }
        }

        private void CalculateRefractionLocationCostDetails(List<BeneficiaryRelationShip> relations, decimal cost, VoucherCreatedEvent notification, Queue<(int BeneficiaryId, decimal Cost)> queue, List<BeneficiaryRelationShip> allRelations, List<Beneficiary> allBeneficiaries, List<BeneficiaryEmplyee> allBeneficiaryEmployees, List<CreateCostCommand> batchCommands, CancellationToken cancellationToken)
        {
            try
            {
                var pc = new PersianCalendar();
                var voucherDate = notification.Item.VoucherDate ?? DateTime.Now;
                var firstDayOfMonth = pc.ToDateTime(pc.GetYear(voucherDate), pc.GetMonth(voucherDate), 1, 0, 0, 0, 0);

                var relationLocations = relations.Select(p => p.ToLocation).ToList();
                var beneficiaryLocations = allBeneficiaries
                .Where(q => relationLocations.Contains(q.Location))
                .Select(p => p.Id)
                .ToList();

                if (relations.Any(q => q.RefractionLevel == RefractionLevel.EnablingTeam))
                {
                    var excludedBeneficiaries = allRelations
                    .Where(q => q.RefractionLevel == RefractionLevel.EnablingTeam || q.RefractionLevel == RefractionLevel.Enabling)
                    .Select(p => p.FromBeneficiaryId)
                    .ToList();

                    beneficiaryLocations = beneficiaryLocations.Where(q => !excludedBeneficiaries.Contains(q)).ToList();
                }

                var beneficiaryEmployees = allBeneficiaryEmployees
                .Where(q => q.Date >= firstDayOfMonth && q.Date <= firstDayOfMonth.AddDays(29) && beneficiaryLocations.Contains(q.BeneficiaryId))
                .ToList();

                var allEmployeeCount = beneficiaryEmployees.Sum(q => q.EmployeeCount);

                foreach (var beneficiary in beneficiaryEmployees)
                {
                    var amount = (cost * beneficiary.EmployeeCount) / allEmployeeCount;
                    var command = new CreateCostCommand
                    {
                        Amount = amount,
                        RefractionLevel = relations.First().RefractionLevel,
                        BeneficiaryId = beneficiary.BeneficiaryId,
                        CostTypeId = notification.Item.CostTypeId,
                        FromBeneficiaryId = relations.First().FromBeneficiaryId,
                        VoucherDate = notification.Item.VoucherDate,
                        VoucherId = notification.Item.Id
                    };

                    batchCommands.Add(command);
                    queue.Enqueue((beneficiary.BeneficiaryId, amount));
                }

                //if (batchCommands.Any())
                //{
                //    var batchCommand = new CreateCostWithBatchCommand { Requests = batchCommands };
                //    await _sender.Send(batchCommand, cancellationToken);
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message, ex);
            }
        }
    }
}
