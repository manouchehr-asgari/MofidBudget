using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Mappings;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.Vouchers.Queries.GetBeneficiary;
namespace MofidBudget.Application.Vouchers.Queries.GetVouchers;

public record GetVouchersByCostTypeWithPaginationQuery : IRequest<PaginatedList<VoucherDto>>
{
    public int CostTypeId { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetVouchersByCostTypeWithPaginationQueryHandler : IRequestHandler<GetVouchersByCostTypeWithPaginationQuery, PaginatedList<VoucherDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVouchersByCostTypeWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<VoucherDto>> Handle(GetVouchersByCostTypeWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Vouchers
            .Where(q=>q.CostTypeId==request.CostTypeId)
            .OrderBy(x => x.VoucherDate)
            .ProjectTo<VoucherDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
