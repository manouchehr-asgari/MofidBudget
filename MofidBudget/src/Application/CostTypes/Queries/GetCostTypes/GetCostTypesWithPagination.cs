using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Mappings;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.CostGroups.Queries.GetCostGroups;
namespace MofidBudget.Application.CostTypes.Queries.GetCostTypes;

public record GetCostTypesWithPaginationQuery : IRequest<PaginatedList<CostTypeDto>>
{
    public int CostGroupId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCostTypesWithPaginationQueryHandler : IRequestHandler<GetCostTypesWithPaginationQuery, PaginatedList<CostTypeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCostTypesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CostTypeDto>> Handle(GetCostTypesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.CostTypes
            .Where(x => x.CostGroupId == request.CostGroupId)
            .OrderBy(x => x.Title)
            .ProjectTo<CostTypeDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
