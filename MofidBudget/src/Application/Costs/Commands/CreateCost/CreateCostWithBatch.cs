using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Entities;
using MofidBudget.Domain.Enums;

namespace MofidBudget.Application.Costs.Commands.CreateCost;


public record CreateCostWithBatchCommand : IRequest
{
    public required List<CreateCostCommand> Requests { get; set; } 
    
}
public class CreateCostWithBatchCommandHandler : IRequestHandler<CreateCostWithBatchCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateCostWithBatchCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateCostWithBatchCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = request.Requests.Select(q=> new Cost
            {
                CostTypeId = q.CostTypeId,
                BeneficiaryId = q.BeneficiaryId,
                BussinessLine=q.BussinessLine,
                VoucherId = q.VoucherId,
                FromBeneficiaryId = q.FromBeneficiaryId,
                Amount = q.Amount,
                RefractionLevel = q.RefractionLevel,
                VoucherDate = q.VoucherDate,
            });

            _context.Costs.AddRange(entities);

            await _context.SaveChangesAsync(cancellationToken);

           
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            throw;
        }
       
    }
}

