using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Domain.Entities;
using MofidBudget.Domain.Enums;

namespace MofidBudget.Application.Vouchers.Queries.GetBeneficiary;
public class VoucherDto
{
    public int Id { get; set; }
    public int CostTypeId { get; set; }
    public DateTime? VoucherDate { get; set; }
    public int BeneficiaryId { get; set; }
    public decimal Cost { get; set; }
    public string? Description { get; set; }
    public string? CompanyName { get; set; }
    public string? AccountTitle { get; set; }
    public string? AccountCode { get; set; }
    public int VoucherNumber { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Voucher, VoucherDto>();
        }
    }
}
