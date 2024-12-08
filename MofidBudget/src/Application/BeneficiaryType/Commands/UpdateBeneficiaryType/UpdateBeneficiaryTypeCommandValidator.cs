using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.BeneficiaryType.Commands.UpdateBeneficiaryType;
public class UpdateBeneficiaryTypeCommandValidator : AbstractValidator<UpdateBeneficiaryTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBeneficiaryTypeCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
       
    }

    public async Task<bool> BeUniqueTitle(UpdateBeneficiaryTypeCommand model, string title, CancellationToken cancellationToken)
    {
        return await _context.BeneficiaryTypes
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Title != title, cancellationToken);
    }
    
}
