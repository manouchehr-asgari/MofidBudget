using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Security;
using MofidBudget.Application.CostCategories.Queries.GetCostCategories;

namespace MofidBudget.Application.CostGroups.Queries.GetCostGroups;
[Authorize]
public record GetCostGroupsQuery : IRequest<IReadOnlyCollection<CostGroupDto>>
{
    public int CostCategoryId { get; init; }
}

public class GetCostGroupsQueryHandler : IRequestHandler<GetCostGroupsQuery, IReadOnlyCollection<CostGroupDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCostGroupsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<CostGroupDto>> Handle(GetCostGroupsQuery request, CancellationToken cancellationToken)
    {
        return await _context.CostGroups
                .Where(x => x.CostCategoryId == request.CostCategoryId)
                .ProjectTo<CostGroupDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken);
    }
}
