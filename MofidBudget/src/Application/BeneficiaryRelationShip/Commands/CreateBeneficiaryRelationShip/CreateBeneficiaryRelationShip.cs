using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Entities;
using MofidBudget.Domain.Enums;

namespace MofidBudget.Application.BeneficiaryRelationShips.Commands.CreateBeneficiaryRelationShip;

public record CreateBeneficiaryRelationShipCommand : IRequest<int>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int FromBeneficiaryId { get; set; }
    public int? ToBeneficiaryId { get; set; }
    public bool? IsActive { get; set; }
    public decimal? Percent { get; set; }
    public RefractionLevel RefractionLevel { get; set; }
    public RefractionType RefractionType { get; set; }
    public Location ToLocation { get; set; }
    public BussinessLine ToBussinessLine { get; set; }
}

public class CreateBeneficiaryRelationShipCommandHandler : IRequestHandler<CreateBeneficiaryRelationShipCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBeneficiaryRelationShipCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBeneficiaryRelationShipCommand request, CancellationToken cancellationToken)
    {
        
        var entity = new BeneficiaryRelationShip
        {
            FromBeneficiaryId = request.FromBeneficiaryId,
            ToBeneficiaryId = request.ToBeneficiaryId,
            FromDate= request.FromDate,
            ToDate= request.ToDate,
            IsActive = request.IsActive,
            Percent = request.Percent,
            RefractionLevel = request.RefractionLevel,
            RefractionType = request.RefractionType,
            ToLocation = request.ToLocation,
            ToBussinessLine=request.ToBussinessLine

        };
        _context.BeneficiaryRelationShips.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

