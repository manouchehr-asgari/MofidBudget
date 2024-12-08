using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.BeneficiaryEmplyees.Commands.CreateBeneficiaryEmplyee;
public class CreateBeneficiaryEmplyeeCommandValidator : AbstractValidator<CreateBeneficiaryEmplyeeCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateBeneficiaryEmplyeeCommandValidator(IApplicationDbContext context)
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
