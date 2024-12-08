using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Enums;

namespace MofidBudget.Application.BeneficiaryRelationShips.Commands.UpdateBeneficiaryRelationShip;
public record UpdateBeneficiaryRelationShipCommand : IRequest
{
    public int Id { get; init; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int FromBeneficiaryId { get; set; }
    public int ToBeneficiaryId { get; set; }
    public bool? IsActive { get; set; }
    public int Percent { get; set; }
    public RefractionLevel RefractionLevel { get; set; }
    public RefractionType RefractionType { get; set; }
    public Location ToLocation { get; set; }

}

public class UpdateBeneficiaryRelationShipCommandHandler : IRequestHandler<UpdateBeneficiaryRelationShipCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBeneficiaryRelationShipCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBeneficiaryRelationShipCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BeneficiaryRelationShips
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        entity.FromBeneficiaryId = request.FromBeneficiaryId;
        entity.ToBeneficiaryId = request.ToBeneficiaryId;
        entity.RefractionType = request.RefractionType;
        entity.FromDate = request.FromDate;
        entity.ToDate = request.ToDate;
        entity.IsActive = request.IsActive;
        entity.Percent = request.Percent;
        entity.RefractionLevel = request.RefractionLevel;
        entity.ToLocation = request.ToLocation;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

