using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Common.Mappings;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.CostGroups.Queries.GetCostGroups;
using MofidBudget.Domain.Entities;
using MofidBudget.Domain.Enums;
namespace MofidBudget.Application.BeneficiaryRelationShips.Queries.GetBeneficiaryRelationShips;

public class BeneficiaryRelationShipDto
{
    public int Id { get; init; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int FromBeneficiaryId { get; set; }
    public int ToBeneficiaryId { get; set; }
    public bool? IsActive { get; set; }
    public int Percent { get; set; }
    public RefractionLevel RefractionLevel { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<BeneficiaryRelationShip, BeneficiaryRelationShipDto>();
        }
    }
}
