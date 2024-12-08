using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.CostGroups.Commands.UpdateCostGroup;


public class UpdateCostGroupCommandValidator : AbstractValidator<UpdateCostGroupCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCostGroupCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200);
            
        RuleFor(v => v.Code)
         .NotEmpty()
         .MaximumLength(200)
         .MustAsync(BeUniqueCode)
             .WithMessage("'{PropertyName}' must be unique.")
             .WithErrorCode("Unique");
    }

    
    public async Task<bool> BeUniqueCode(UpdateCostGroupCommand model, string code, CancellationToken cancellationToken)
    {
        return await _context.CostGroups
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Code != code, cancellationToken);
    }
}
