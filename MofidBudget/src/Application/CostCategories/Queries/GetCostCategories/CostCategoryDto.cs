using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.CostGroups.Queries.GetCostGroups;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Application.CostCategories.Queries.GetCostCategories;
public class CostCategoryDto
{
    public CostCategoryDto()
    {
        Items = Array.Empty<CostGroupDto>();
    }

    public int Id { get; init; }

    public string? Title { get; init; }
    public string? Code { get; init; }

    public IReadOnlyCollection<CostGroupDto> Items { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CostCategory, CostCategoryDto>();
        }
    }
}
