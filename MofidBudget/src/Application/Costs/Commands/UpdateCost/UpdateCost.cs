using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Enums;

namespace MofidBudget.Application.Costs.Commands.UpdateCost;
public record UpdateCostCommand : IRequest
{
    public int Id { get; set; }
    public int CostTypeId { get; set; }
    public DateTime? VoucherDate { get; set; }
    public int BeneficiaryId { get; set; }
    public int FromBeneficiaryId { get; set; }
    public decimal Amount { get; set; }
    public RefractionLevel RefractionLevel { get; set; }
    public int VoucherId { get; set; }

}

public class UpdateCostCommandHandler : IRequestHandler<UpdateCostCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Costs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        entity.CostTypeId = request.CostTypeId;
        entity.BeneficiaryId = request.BeneficiaryId;
        entity.VoucherId = request.VoucherId;
        entity.FromBeneficiaryId = request.FromBeneficiaryId;
        entity.Amount = request.Amount;
        entity.RefractionLevel = request.RefractionLevel;
        entity.VoucherDate = request.VoucherDate;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

