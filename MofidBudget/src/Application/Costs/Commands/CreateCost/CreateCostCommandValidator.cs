using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.Costs.Commands.CreateCost;
public class CreateCostCommandValidator : AbstractValidator<CreateCostCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateCostCommandValidator(IApplicationDbContext context)
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
      
    }
}
