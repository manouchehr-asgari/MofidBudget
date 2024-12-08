using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Beneficiaries.Commands.UpdateBeneficiary;

namespace MofidBudget.Application.Beneficiaries.Commands.UpdateBeneficiary;
public class UpdateBeneficiaryCommandValidator : AbstractValidator<UpdateBeneficiaryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBeneficiaryCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200);
        RuleFor(v => v.BeneficiaryTypeId)
           .NotEmpty();
        RuleFor(v => v.BeneficiaryGroupId)
           .NotEmpty();
        RuleFor(v => v.Location)
        .NotEmpty();
        RuleFor(v => v.Code)
         .NotEmpty()
         .MaximumLength(200)
         .MustAsync(BeUniqueCode)
             .WithMessage("'{PropertyName}' must be unique.")
             .WithErrorCode("Unique");
    }


    public async Task<bool> BeUniqueCode(UpdateBeneficiaryCommand model, string code, CancellationToken cancellationToken)
    {
        return await _context.Beneficiaries
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Code != code||l.BeneficiaryGroupId!=model.BeneficiaryGroupId, cancellationToken);
    }
}
