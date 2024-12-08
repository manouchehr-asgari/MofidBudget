using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.BeneficiaryRelationShips.Commands.CreateBeneficiaryRelationShip;
public class CreateBeneficiaryRelationShipCommandValidator : AbstractValidator<CreateBeneficiaryRelationShipCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateBeneficiaryRelationShipCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.FromBeneficiaryId)
            .NotEmpty();
        
        RuleFor(v => v.RefractionType)
         .NotEmpty();
        RuleFor(v => v.RefractionLevel)
         .NotEmpty();

    }
}
