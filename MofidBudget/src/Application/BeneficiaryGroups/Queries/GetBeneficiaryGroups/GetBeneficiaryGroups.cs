using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Security;

namespace MofidBudget.Application.BeneficiaryGroups.Queries.GetBeneficiaryGroups;

[Authorize]
public record GetBeneficiaryGroupsQuery : IRequest<IReadOnlyCollection<BeneficiaryGroupDto>>;

public class GetBeneficiaryGroupsQueryHandler : IRequestHandler<GetBeneficiaryGroupsQuery, IReadOnlyCollection<BeneficiaryGroupDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBeneficiaryGroupsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<BeneficiaryGroupDto>> Handle(GetBeneficiaryGroupsQuery request, CancellationToken cancellationToken)
    {
        return await _context.BeneficiaryGroups
                .AsNoTracking()
                .ProjectTo<BeneficiaryGroupDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken);
    }
}

