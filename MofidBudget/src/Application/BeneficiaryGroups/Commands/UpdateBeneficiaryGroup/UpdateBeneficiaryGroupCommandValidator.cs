using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.BeneficiaryGroups.Commands.UpdateBeneficiaryGroup;

namespace MofidBudget.Application.BeneficiaryGroups.Commands.UpdateBeneficiaryGroup;
public class UpdateBeneficiaryGroupCommandValidator : AbstractValidator<UpdateBeneficiaryGroupCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBeneficiaryGroupCommandValidator(IApplicationDbContext context)
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

    public async Task<bool> BeUniqueTitle(UpdateBeneficiaryGroupCommand model, string title, CancellationToken cancellationToken)
    {
        return await _context.BeneficiaryGroups
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Title != title, cancellationToken);
    }
    public async Task<bool> BeUniqueCode(UpdateBeneficiaryGroupCommand model, string code, CancellationToken cancellationToken)
    {
        return await _context.BeneficiaryGroups
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Code != code, cancellationToken);
    }
}
