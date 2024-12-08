using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Security;

namespace MofidBudget.Application.CostCategories.Queries.GetCostCategories;

[Authorize]
public record GetCostCategoriesQuery : IRequest<IReadOnlyCollection<CostCategoryDto>>;

public class GetCostCategoriesQueryHandler : IRequestHandler<GetCostCategoriesQuery, IReadOnlyCollection<CostCategoryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCostCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<CostCategoryDto>> Handle(GetCostCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _context.CostCategories
                .AsNoTracking()
                .ProjectTo<CostCategoryDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken);
    }
}

