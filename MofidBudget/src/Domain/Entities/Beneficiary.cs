using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MofidBudget.Domain.Entities;
public class Beneficiary : BaseAuditableEntity
{
    public string? Title { get; set; }
    public  string? Code { get; set; }
    public int BeneficiaryTypeId { get; set; }
   // public BeneficiaryType BeneficiaryType { get; set; } = null!;
    public int BeneficiaryGroupId { get; set; }
   // public BeneficiaryGroup BeneficiaryGroup { get; set; } = null!;
    public Location Location { get; set; }
}
