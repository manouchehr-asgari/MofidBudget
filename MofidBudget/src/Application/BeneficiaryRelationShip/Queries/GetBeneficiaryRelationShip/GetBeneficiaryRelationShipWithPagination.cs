using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Mappings;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.CostGroups.Queries.GetCostGroups;
namespace MofidBudget.Application.BeneficiaryRelationShips.Queries.GetBeneficiaryRelationShips;

public record GetBeneficiaryRelationShipsWithPaginationQuery : IRequest<PaginatedList<BeneficiaryRelationShipDto>>
{
    public int CostGroupId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetBeneficiaryRelationShipsWithPaginationQueryHandler : IRequestHandler<GetBeneficiaryRelationShipsWithPaginationQuery, PaginatedList<BeneficiaryRelationShipDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBeneficiaryRelationShipsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BeneficiaryRelationShipDto>> Handle(GetBeneficiaryRelationShipsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.BeneficiaryRelationShips
            .OrderBy(x => x.FromBeneficiaryId)
            .ProjectTo<BeneficiaryRelationShipDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
