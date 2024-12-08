using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MofidBudget.Domain.Entities;
public class BeneficiaryRelationShip : BaseAuditableEntity
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int FromBeneficiaryId { get; set; }
    public Beneficiary FromBeneficiary { get; set; } = null!;
    public int? ToBeneficiaryId { get; set; }
    public Beneficiary ToBeneficiary { get; set; } = null!;
    public bool? IsActive { get; set; }
    public decimal? Percent { get; set; }
    public RefractionLevel RefractionLevel { get; set; }
    public RefractionType RefractionType { get; set; }
    public Location? ToLocation { get; set; }
    public BussinessLine? ToBussinessLine { get; set; }
}
