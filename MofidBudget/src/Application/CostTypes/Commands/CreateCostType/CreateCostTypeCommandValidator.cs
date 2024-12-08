using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.CostTypes.Commands.CreateCostType;
public class CreateCostTypeCommandValidator : AbstractValidator<CreateCostTypeCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateCostTypeCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.Code)
            .NotEmpty()
           .MaximumLength(200)
           .MustAsync(BeUniqueCode)
               .WithMessage("'{PropertyName}' must be unique.")
               .WithErrorCode("Unique");
    }
    public async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken)
    {
        return await _context.CostTypes
            .AllAsync(l => l.Code != code, cancellationToken);
    }
}
