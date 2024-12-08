using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MofidBudget.Domain.Entities;

public class Cost : BaseAuditableEntity
{
    public int CostTypeId { get; set; }
    public DateTime? VoucherDate { get; set; }
    public CostType CostType { get; set; } = null!;
    public int? BeneficiaryId { get; set; }
    public Beneficiary Beneficiary { get; set; } = null!;
    public int? FromBeneficiaryId { get; set; }
    public Beneficiary FromBeneficiary { get; set; } = null!;
    public decimal Amount { get; set; }
    public RefractionLevel RefractionLevel { get; set; }
    public BussinessLine? BussinessLine { get; set; }
    public Voucher Voucher { get; set; } = null!;
    public int VoucherId { get; set; }
    
   
   
}
