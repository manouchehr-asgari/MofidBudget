using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.CostTypes.Commands.UpdateCostType;
public record UpdateCostTypeCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
    public string Code { get; init; } = string.Empty;
    public int CostGroupId { get; init; }

}

public class UpdateCostTypeCommandHandler : IRequestHandler<UpdateCostTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCostTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCostTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CostTypes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        entity.Title = request.Title;
        entity.Code = request.Code;
        entity.CostGroupId = request.CostGroupId;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

