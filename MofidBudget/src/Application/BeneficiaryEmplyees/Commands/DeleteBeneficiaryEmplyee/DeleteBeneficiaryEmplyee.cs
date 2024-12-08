using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.BeneficiaryEmplyees.Commands.DeleteBeneficiaryEmplyee;


public record DeleteBeneficiaryEmplyeeCommand(int Id) : IRequest;

public class DeleteBeneficiaryEmplyeeCommandHandler : IRequestHandler<DeleteBeneficiaryEmplyeeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBeneficiaryEmplyeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBeneficiaryEmplyeeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BeneficiaryEmplyees
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.BeneficiaryEmplyees.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
