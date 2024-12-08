using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.BeneficiaryType.Commands.DeleteBeneficiaryType;
public record DeleteBeneficiaryTypeCommand(int Id) : IRequest;
public class DeleteBeneficiaryTypeCommandHandler : IRequestHandler<DeleteBeneficiaryTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBeneficiaryTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBeneficiaryTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BeneficiaryTypes
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.BeneficiaryTypes.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
