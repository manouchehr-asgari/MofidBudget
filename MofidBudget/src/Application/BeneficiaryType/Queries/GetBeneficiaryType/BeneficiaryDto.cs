using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Domain.Entities;
using MofidBudget.Domain.Enums;

namespace MofidBudget.Application.BeneficiaryType.Queries.GetBeneficiaryTypes;

public class BeneficiaryDto
{
   
    public int Id { get; init; }
    public string? BeneficiaryGroup { get; init; }
    public string? Title { get; init; }
    public string? Code { get; init; }
    public Location Location { get; set; }
    public DateTimeOffset? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Beneficiary, BeneficiaryDto>();
        }
    }
}
