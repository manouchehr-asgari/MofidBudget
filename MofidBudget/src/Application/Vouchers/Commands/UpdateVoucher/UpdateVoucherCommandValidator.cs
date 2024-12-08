using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Vouchers.Commands.UpdateVoucher;

namespace MofidBudget.Application.Vouchers.Commands.UpdateVoucher;
public class UpdateVoucherCommandValidator : AbstractValidator<UpdateVoucherCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateVoucherCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.BeneficiaryId)
            .NotEmpty();
        RuleFor(v => v.CostTypeId)
           .NotEmpty();
        RuleFor(v => v.Amount)
           .NotEmpty();
        RuleFor(v => v.VoucherDate)
        .NotEmpty();
        RuleFor(v => v.Description)
         .MaximumLength(1000);
        RuleFor(v => v.CompanyName)
         .MaximumLength(200);
        RuleFor(v => v.AccountCode)
         .MaximumLength(200);
        RuleFor(v => v.AccountTitle)
         .MaximumLength(200);
    }

   
  
   


    
}
