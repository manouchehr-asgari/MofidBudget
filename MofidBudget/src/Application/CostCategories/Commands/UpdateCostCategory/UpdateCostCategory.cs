using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.CostCategories.Commands.UpdateCostCategory;
public record UpdateCostCategoryCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
    public required string Code { get; init; }
}

public class UpdateCostCategoryCommandHandler : IRequestHandler<UpdateCostCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCostCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCostCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CostCategories
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;
        entity.Code = request.Code;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
