using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Entities;
using MofidBudget.Domain.Enums;

namespace MofidBudget.Application.Costs.Commands.CreateCost;

public record CreateCostCommand : IRequest<int>
{
    public int CostTypeId { get; set; }
    public DateTime? VoucherDate { get; set; }
    public int? BeneficiaryId { get; set; }
    public BussinessLine? BussinessLine { get; set; }
    public int? FromBeneficiaryId { get; set; }
    public decimal Amount { get; set; }
    public RefractionLevel RefractionLevel { get; set; }
    public int VoucherId { get; set; }
}

public class CreateCostCommandHandler : IRequestHandler<CreateCostCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new Cost
            {
                CostTypeId = request.CostTypeId,
                BeneficiaryId = request.BeneficiaryId,
                BussinessLine = request.BussinessLine,
                VoucherId = request.VoucherId,
                FromBeneficiaryId = request.FromBeneficiaryId,
                Amount = request.Amount,
                RefractionLevel = request.RefractionLevel,
                VoucherDate = request.VoucherDate,
            };

            _context.Costs.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            throw;
        }
       
    }
}

