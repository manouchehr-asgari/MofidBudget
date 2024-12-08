using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Events;

namespace MofidBudget.Application.CostGroups.Commands.DeleteCostGroup;

public record DeleteCostGroupCommand(int Id) : IRequest;

public class DeleteCostGroupCommandHandler : IRequestHandler<DeleteCostGroupCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCostGroupCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCostGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CostGroups
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.CostGroups.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
