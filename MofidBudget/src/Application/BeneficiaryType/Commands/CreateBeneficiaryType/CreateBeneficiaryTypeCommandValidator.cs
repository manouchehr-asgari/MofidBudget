using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.BeneficiaryType.Commands.CreateBeneficiaryType;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.BeneficiaryType.Commands.CreateBeneficiaryTypes;
public class CreateBeneficiaryTypeCommandValidator : AbstractValidator<CreateBeneficiaryTypeCommand>
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

       
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.BeneficiaryTypes
            .AllAsync(l => l.Title != title, cancellationToken);
    }
   
}
