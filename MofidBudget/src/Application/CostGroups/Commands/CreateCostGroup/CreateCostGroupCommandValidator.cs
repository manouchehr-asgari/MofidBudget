using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.CostGroups.Commands.CreateCostGroup;
public class CreateCostGroupCommandValidator : AbstractValidator<CreateCostGroupCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateCostGroupCommandValidator(IApplicationDbContext context)
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
        return await _context.CostGroups
            .AllAsync(l => l.Code != code, cancellationToken);
    }
}
