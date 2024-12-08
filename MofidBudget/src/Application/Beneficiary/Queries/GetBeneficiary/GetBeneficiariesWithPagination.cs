using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.BeneficiaryType.Queries.GetBeneficiaryTypes;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Mappings;
using MofidBudget.Application.Common.Models;
namespace MofidBudget.Application.Beneficiaries.Queries.GetBeneficiaries;

public record GetBeneficiariesWithPaginationQuery : IRequest<PaginatedList<BeneficiaryDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetBeneficiariesWithPaginationQueryHandler : IRequestHandler<GetBeneficiariesWithPaginationQuery, PaginatedList<BeneficiaryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBeneficiariesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BeneficiaryDto>> Handle(GetBeneficiariesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Beneficiaries
            .Join(_context.BeneficiaryGroups,
            b => b.BeneficiaryGroupId,
            bg => bg.Id,
            (b, bg) => new BeneficiaryDto
            {
                Id=b.Id,
                Title = b.Title,
                Code = b.Code,
                BeneficiaryGroup = bg.Title,
                Location= b.Location,
                Created = b.Created,
                CreatedBy=b.CreatedBy,
                LastModified=b.LastModified,
                LastModifiedBy = b.LastModifiedBy
            })
            .OrderBy(x => x.Title)
            //.ProjectTo<BeneficiaryDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
