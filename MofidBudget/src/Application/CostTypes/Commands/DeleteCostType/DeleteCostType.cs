using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.CostTypes.Commands.DeleteCostType;


public record DeleteCostTypeCommand(int Id) : IRequest;

public class DeleteCostTypeCommandHandler : IRequestHandler<DeleteCostTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCostTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCostTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CostTypes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.CostTypes.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
