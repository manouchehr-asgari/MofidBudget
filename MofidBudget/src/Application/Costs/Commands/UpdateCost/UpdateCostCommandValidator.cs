using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.Costs.Commands.UpdateCost;

namespace MofidBudget.Application.Costs.Commands.UpdateCost;
public class UpdateCostCommandValidator : AbstractValidator<UpdateCostCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCostCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.RefractionLevel)
            .NotEmpty();
        RuleFor(v => v.CostTypeId)
           .NotEmpty();
        RuleFor(v => v.Amount)
           .NotEmpty();
        RuleFor(v => v.VoucherDate)
        .NotEmpty();
        RuleFor(v => v.BeneficiaryId)
         .NotEmpty();
       
    }







}
