using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Security;
namespace MofidBudget.Application.BeneficiaryType.Queries.GetBeneficiaryTypes;

[Authorize]
public record GetBeneficiaryTypesQuery : IRequest<IReadOnlyCollection<BeneficiaryTypeDto>>;

public class GetBeneficiaryTypesQueryHandler : IRequestHandler<GetBeneficiaryTypesQuery, IReadOnlyCollection<BeneficiaryTypeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBeneficiaryTypesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<BeneficiaryTypeDto>> Handle(GetBeneficiaryTypesQuery request, CancellationToken cancellationToken)
    {
        return await _context.BeneficiaryTypes
                .AsNoTracking()
                .ProjectTo<BeneficiaryTypeDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken);
    }
}

