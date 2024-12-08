using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Beneficiaries.Commands.UpdateBeneficiary;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.Beneficiaries.Commands.CreateBeneficiary;
public class CreateBeneficiaryCommandValidator : AbstractValidator<CreateBeneficiaryCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateBeneficiaryCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.BeneficiaryTypeId)
           .NotEmpty();
        RuleFor(v => v.BeneficiaryGroupId)
           .NotEmpty();
        
        RuleFor(v => v.Code)
            .NotEmpty()
           .MaximumLength(200)
           .MustAsync(BeUniqueCode)
               .WithMessage("'{PropertyName}' must be unique.")
               .WithErrorCode("Unique");
    }
    public async Task<bool> BeUniqueCode(CreateBeneficiaryCommand model, string code, CancellationToken cancellationToken)
    {
        return await _context.Beneficiaries
            .AllAsync(l => l.Code != code || l.BeneficiaryGroupId != model.BeneficiaryGroupId, cancellationToken);
    }
}
