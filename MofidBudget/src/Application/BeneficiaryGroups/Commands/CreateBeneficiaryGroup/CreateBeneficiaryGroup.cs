using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Application.BeneficiaryGroups.Commands.BeneficiaryGroup;


namespace MofidBudget.Application.BeneficiaryGroups.Commands.BeneficiaryGroup;
public record CreateBeneficiaryGroupCommand : IRequest<int>
{
    public required string Title { get; init; }
    public required string Code { get; init; }
}
public class BeneficiaryGroupCommandHandler : IRequestHandler<CreateBeneficiaryGroupCommand, int>
{
    private readonly IApplicationDbContext _context;

    public BeneficiaryGroupCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBeneficiaryGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.BeneficiaryGroup
        {
            Title = request.Title,
            Code = request.Code
        };
        _context.BeneficiaryGroups.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
