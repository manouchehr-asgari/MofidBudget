using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.CostGroups.Commands.UpdateCostGroup;

public record UpdateCostGroupCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
    public string Code { get; init; } = string.Empty;
    public int CostCategoryId { get; init; }

}

public class UpdateCostGroupCommandHandler : IRequestHandler<UpdateCostGroupCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCostGroupCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCostGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CostGroups
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        entity.Title = request.Title;
        entity.Code = request.Code;
        entity.CostCategoryId = request.CostCategoryId;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
