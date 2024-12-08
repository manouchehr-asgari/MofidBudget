using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Mappings;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.Costs.Queries.GetCosts;

public record GetCostsQuery : IRequest<PaginatedList<CostDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCostsQueryHandler : IRequestHandler<GetCostsQuery, PaginatedList<CostDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCostsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CostDto>> Handle(GetCostsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Costs
            
            .OrderBy(x => x.Beneficiary)
            .ProjectTo<CostDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
