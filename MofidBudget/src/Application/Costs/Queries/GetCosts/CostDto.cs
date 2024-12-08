using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.CostGroups.Queries.GetCostGroups;
using MofidBudget.Domain.Entities;
using MofidBudget.Domain.Enums;

namespace MofidBudget.Application.Costs.Queries.GetCosts;
public class CostDto
{
    public int Id { get; set; }
    public int CostTypeId { get; set; }
    public DateTime? VoucherDate { get; set; }
    public int BeneficiaryId { get; set; }
    public int FromBeneficiaryId { get; set; }
    public decimal Amount { get; set; }
    public RefractionLevel RefractionLevel { get; set; }
    public int VoucherId { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Cost, CostDto>();
        }
    }
}
