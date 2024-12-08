using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.Costs.Commands.DeleteCost;


public record DeleteCostCommand(int Id) : IRequest;

public class DeleteCostCommandHandler : IRequestHandler<DeleteCostCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Costs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Costs.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
