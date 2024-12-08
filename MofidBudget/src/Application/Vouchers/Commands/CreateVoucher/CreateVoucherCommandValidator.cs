using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.Vouchers.Commands.CreateVoucher;
public class CreateVoucherCommandValidator : AbstractValidator<CreateVoucherCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateVoucherCommandValidator(IApplicationDbContext context)
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
