using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.BeneficiaryGroups.Commands.UpdateBeneficiaryGroup;
public record UpdateBeneficiaryGroupCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
    public required string Code { get; init; }
}

public class UpdateBeneficiaryGroupCommandHandler : IRequestHandler<UpdateBeneficiaryGroupCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBeneficiaryGroupCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBeneficiaryGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BeneficiaryGroups
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;
        entity.Code = request.Code;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
