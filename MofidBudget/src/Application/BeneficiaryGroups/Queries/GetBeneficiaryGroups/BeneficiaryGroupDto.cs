using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.BeneficiaryType.Queries.GetBeneficiaryTypes;
using MofidBudget.Application.CostGroups.Queries.GetCostGroups;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Application.BeneficiaryGroups.Queries.GetBeneficiaryGroups;
public class BeneficiaryGroupDto
{
    public BeneficiaryGroupDto()
    {
        Items = Array.Empty<BeneficiaryDto>();
    }

    public int Id { get; init; }

    public string? Title { get; init; }
    public string? Code { get; init; }

    public IReadOnlyCollection<BeneficiaryDto> Items { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<BeneficiaryGroup, BeneficiaryGroupDto>();
        }
    }
}
