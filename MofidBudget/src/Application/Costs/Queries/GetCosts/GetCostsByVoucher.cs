using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Mappings;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.Costs.Queries.GetCosts;

public record GetCostsByVoucherQuery : IRequest<PaginatedList<CostDto>>
{
    public int VoucherId { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCostByVoucherIdQueryHandler : IRequestHandler<GetCostsByVoucherQuery, PaginatedList<CostDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCostByVoucherIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CostDto>> Handle(GetCostsByVoucherQuery request, CancellationToken cancellationToken)
    {
        return await _context.Costs
            .Where(q=>q.VoucherId==request.VoucherId)
            .OrderBy(x => x.VoucherDate)
            .ProjectTo<CostDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
