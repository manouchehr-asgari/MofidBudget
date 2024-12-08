using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Mappings;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.CostCategories.Queries.GetCostCategories;
using MofidBudget.Application.BeneficiaryEmplyees.Queries.GetBeneficiaryEmplyees;
namespace MofidBudget.Application.BeneficiaryEmplyees.Queries.GetBeneficiaryEmplyees;

public record GetBeneficiaryEmplyeesQuery : IRequest<IReadOnlyCollection<BeneficiaryEmplyeeDto>>
{
    public int Id { get; init; }

    public int BeneficiaryId { get; init; }
    public DateTime? Date { get; set; }
    public int EmployeeCount { get; set; }
}

public class GetBeneficiaryEmplyeesQueryHandler : IRequestHandler<GetBeneficiaryEmplyeesQuery, IReadOnlyCollection<BeneficiaryEmplyeeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBeneficiaryEmplyeesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<BeneficiaryEmplyeeDto>> Handle(GetBeneficiaryEmplyeesQuery request, CancellationToken cancellationToken)
    {
        return await _context.BeneficiaryEmplyees
                .ProjectTo<BeneficiaryEmplyeeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
    }
}
