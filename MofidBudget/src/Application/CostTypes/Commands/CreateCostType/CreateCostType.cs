using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Application.CostTypes.Commands.CreateCostType;

public record CreateCostTypeCommand : IRequest<int>
{
    public int CostGroupId { get; init; }
    public string Code { get; init; } = string.Empty;
    public string? Title { get; init; }
}

public class CreateCostTypeCommandHandler : IRequestHandler<CreateCostTypeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCostTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCostTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new CostType
        {
            CostGroupId = request.CostGroupId,
            Title = request.Title,
            Code= request.Code  
        };

        _context.CostTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

