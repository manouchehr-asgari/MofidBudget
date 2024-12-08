using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.BeneficiaryRelationShips.Commands.DeleteBeneficiaryRelationShip;


public record DeleteBeneficiaryRelationShipCommand(int Id) : IRequest;

public class DeleteBeneficiaryRelationShipCommandHandler : IRequestHandler<DeleteBeneficiaryRelationShipCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBeneficiaryRelationShipCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBeneficiaryRelationShipCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BeneficiaryRelationShips
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.BeneficiaryRelationShips.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
