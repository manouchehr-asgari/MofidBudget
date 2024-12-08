﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.BeneficiaryType.Queries.GetBeneficiaryTypes;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Mappings;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.CostGroups.Queries.GetCostGroups;

public record GetBeneficiariesByTypeWithPaginationQuery : IRequest<PaginatedList<BeneficiaryDto>>
{
    public int TypeId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetBeneficiariesByTypeWithPaginationQueryHandler : IRequestHandler<GetBeneficiariesByTypeWithPaginationQuery, PaginatedList<BeneficiaryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBeneficiariesByTypeWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BeneficiaryDto>> Handle(GetBeneficiariesByTypeWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Beneficiaries
            .Where(x => x.BeneficiaryTypeId == request.TypeId)
            .OrderBy(x => x.Title)
            .ProjectTo<BeneficiaryDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
