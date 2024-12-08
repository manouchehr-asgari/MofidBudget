using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.CostCategories.Commands.CreateCostCategory;
public class CreateBeneficiaryTypeCommandValidator : AbstractValidator<CreateCostCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateBeneficiaryTypeCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");

        RuleFor(v => v.Code)
           .NotEmpty()
           .MaximumLength(200)
           .MustAsync(BeUniqueCode)
               .WithMessage("'{PropertyName}' must be unique.")
               .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.CostCategories
            .AllAsync(l => l.Title != title, cancellationToken);
    }
    public async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken)
    {
        return await _context.CostCategories
            .AllAsync(l => l.Code != code, cancellationToken);
    }
}
