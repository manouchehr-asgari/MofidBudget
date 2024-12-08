using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.CostCategories.Commands.DeleteCostCategory;

namespace MofidBudget.Application.CostCategories.Commands.DeleteCostCategory;
public record DeleteCostCategoryCommand(int Id) : IRequest;
public class DeleteCostCategoryCommandHandler : IRequestHandler<DeleteCostCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCostCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCostCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CostCategories
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.CostCategories.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
