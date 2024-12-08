using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.CostCategories.Commands.CreateCostCategory;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Application.CostCategories.Commands.CreateCostCategory;
public record CreateCostCategoryCommand : IRequest<int>
{
    public required string Title { get; init; }
    public required string Code { get; init; }
}
public class CreateCostCategoryCommandHandler : IRequestHandler<CreateCostCategoryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCostCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCostCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new CostCategory
        {
            Title = request.Title,
            Code = request.Code
        };
        _context.CostCategories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
