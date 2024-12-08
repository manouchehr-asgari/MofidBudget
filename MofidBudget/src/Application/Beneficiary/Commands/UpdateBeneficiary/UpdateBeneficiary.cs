using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Enums;

namespace MofidBudget.Application.Beneficiaries.Commands.UpdateBeneficiary;
public record UpdateBeneficiaryCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
    public string Code { get; init; } = string.Empty;
    public Location Location { get; set; }
    public int BeneficiaryGroupId { get; init; }
    public int BeneficiaryTypeId { get; init; }

}

public class UpdateBeneficiaryCommandHandler : IRequestHandler<UpdateBeneficiaryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBeneficiaryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Beneficiaries
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        entity.Title = request.Title;
        entity.Code = request.Code;
        entity.Location = request.Location;
        entity.BeneficiaryGroupId = request.BeneficiaryGroupId;
        entity.BeneficiaryTypeId = request.BeneficiaryTypeId;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

