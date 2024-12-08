using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.BeneficiaryGroups.Commands.DeleteBeneficiaryGroup;

namespace MofidBudget.Application.BeneficiaryGroups.Commands.DeleteBeneficiaryGroup;
public record DeleteBeneficiaryGroupCommand(int Id) : IRequest;
public class DeleteBeneficiaryGroupCommandHandler : IRequestHandler<DeleteBeneficiaryGroupCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBeneficiaryGroupCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBeneficiaryGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BeneficiaryGroups
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.BeneficiaryGroups.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
