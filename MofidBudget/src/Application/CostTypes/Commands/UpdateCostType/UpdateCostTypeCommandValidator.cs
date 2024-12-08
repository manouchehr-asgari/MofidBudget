using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.CostTypes.Commands.UpdateCostType;

namespace MofidBudget.Application.CostTypes.Commands.UpdateCostType;
public class UpdateCostTypeCommandValidator : AbstractValidator<UpdateCostTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCostTypeCommandValidator(IApplicationDbContext context)
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


    public async Task<bool> BeUniqueCode(UpdateCostTypeCommand model, string code, CancellationToken cancellationToken)
    {
        return await _context.CostTypes
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Code != code, cancellationToken);
    }
}
