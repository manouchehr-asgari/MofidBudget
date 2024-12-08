using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.Beneficiaries.Commands.DeleteBeneficiary;


public record DeleteBeneficiaryCommand(int Id) : IRequest;

public class DeleteBeneficiaryCommandHandler : IRequestHandler<DeleteBeneficiaryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBeneficiaryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Beneficiaries
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Beneficiaries.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
