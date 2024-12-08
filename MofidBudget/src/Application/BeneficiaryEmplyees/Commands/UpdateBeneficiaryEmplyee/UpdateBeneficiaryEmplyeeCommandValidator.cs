using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.BeneficiaryEmplyees.Commands.UpdateBeneficiaryEmplyee;

namespace MofidBudget.Application.BeneficiaryEmplyees.Commands.UpdateBeneficiaryEmplyee;
public class UpdateBeneficiaryEmplyeeCommandValidator : AbstractValidator<UpdateBeneficiaryEmplyeeCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateBeneficiaryEmplyeeCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.BeneficiaryId)
            .NotEmpty();
        RuleFor(v => v.EmployeeCount)
            .NotEmpty()
           .GreaterThan(0);
        RuleFor(v => v.Date)
            .NotEmpty();
    }
}
