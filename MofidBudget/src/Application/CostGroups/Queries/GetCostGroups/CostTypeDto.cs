using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Application.CostGroups.Queries.GetCostGroups;
public class CostTypeDto
{
    public int Id { get; init; }

    public int CostGroupId { get; init; }
    public string? Code { get; init; }

    public string? Title { get; init; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CostType, CostTypeDto>();
        }
    }
}
