using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.BeneficiaryType.Commands.CreateBeneficiaryType;
public record CreateBeneficiaryTypeCommand : IRequest<int>
{
    public required string Title { get; init; }
}
public class CreateBeneficiaryTypeCommandHandler : IRequestHandler<CreateBeneficiaryTypeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBeneficiaryTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBeneficiaryTypeCommand request, CancellationToken cancellationToken)
    {

        var entity = new Domain.Entities.BeneficiaryType
        {
            Title = request.Title

        };

        _context.BeneficiaryTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
