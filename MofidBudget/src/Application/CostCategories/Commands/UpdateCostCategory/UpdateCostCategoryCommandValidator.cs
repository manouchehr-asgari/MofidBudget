using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.CostCategories.Commands.UpdateCostCategory;

namespace MofidBudget.Application.CostCategories.Commands.UpdateCostCategory;
public class UpdateCostCategoryCommandValidator : AbstractValidator<UpdateCostCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCostCategoryCommandValidator(IApplicationDbContext context)
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

    public async Task<bool> BeUniqueTitle(UpdateCostCategoryCommand model, string title, CancellationToken cancellationToken)
    {
        return await _context.CostCategories
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Title != title, cancellationToken);
    }
    public async Task<bool> BeUniqueCode(UpdateCostCategoryCommand model, string code, CancellationToken cancellationToken)
    {
        return await _context.CostCategories
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Code != code, cancellationToken);
    }
}
