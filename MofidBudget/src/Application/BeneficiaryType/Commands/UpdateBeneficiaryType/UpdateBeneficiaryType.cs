using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.BeneficiaryType.Commands.UpdateBeneficiaryType;
public record UpdateBeneficiaryTypeCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateBeneficiaryTypeCommandHandler : IRequestHandler<UpdateBeneficiaryTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBeneficiaryTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBeneficiaryTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BeneficiaryTypes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;
        await _context.SaveChangesAsync(cancellationToken);

    }
}
