using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.BeneficiaryGroups.Commands.BeneficiaryGroup;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.BeneficiaryGroups.Commands.CreateBeneficiaryGroup;
public class CreateBeneficiaryTypeCommandValidator : AbstractValidator<CreateBeneficiaryGroupCommand>
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
        return await _context.BeneficiaryGroups
            .AllAsync(l => l.Title != title, cancellationToken);
    }
    public async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken)
    {
        return await _context.BeneficiaryGroups
            .AllAsync(l => l.Code != code, cancellationToken);
    }
}
