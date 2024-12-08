using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Mappings;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.Costs.Queries.GetCosts;

public record GetCostsByBeneficiaryQuery : IRequest<PaginatedList<CostDto>>
{
    public int BeneficiaryId { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCostByBeneficiaryIdQueryHandler : IRequestHandler<GetCostsByBeneficiaryQuery, PaginatedList<CostDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCostByBeneficiaryIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CostDto>> Handle(GetCostsByBeneficiaryQuery request, CancellationToken cancellationToken)
    {
        return await _context.Costs
            .Where(q=>q.BeneficiaryId==request.BeneficiaryId)
            .OrderBy(x => x.VoucherDate)
            .ProjectTo<CostDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
