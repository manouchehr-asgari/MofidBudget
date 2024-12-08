using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.BeneficiaryRelationShips.Commands.UpdateBeneficiaryRelationShip;

namespace MofidBudget.Application.BeneficiaryRelationShips.Commands.UpdateBeneficiaryRelationShip;
public class UpdateBeneficiaryRelationShipCommandValidator : AbstractValidator<UpdateBeneficiaryRelationShipCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBeneficiaryRelationShipCommandValidator(IApplicationDbContext context)
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
