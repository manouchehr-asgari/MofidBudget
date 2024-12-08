using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MofidBudget.Domain.Entities;

public class Voucher : BaseAuditableEntity
{
    public int CostTypeId { get; set; }
    public DateTime? VoucherDate { get; set; }
    public CostType CostType { get; set; } = null!;
    public int BeneficiaryId { get; set; }
    public Beneficiary Beneficiary { get; set; } = null!;
    public float Amount { get; set; }
    public string? Description { get; set; }
    public string? CompanyName { get; set; }
    public string? AccountTitle { get; set; }
    public string? AccountCode { get; set; }
    public int VoucherNumber { get; set; }
   
   
}
