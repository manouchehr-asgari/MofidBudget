using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MofidBudget.Domain.Entities;
public class BeneficiaryEmplyee : BaseAuditableEntity
{
    public DateTime? Date { get; set; }
    public int EmployeeCount { get; set; }
    public int BeneficiaryId { get; set; }
    public Beneficiary Beneficiary { get; set; } = null!;
}
