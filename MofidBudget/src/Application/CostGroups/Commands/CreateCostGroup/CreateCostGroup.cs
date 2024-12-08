using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Entities;
using MofidBudget.Domain.Events;

namespace MofidBudget.Application.CostGroups.Commands.CreateCostGroup;

public record CreateCostGroupCommand : IRequest<int>
{
    public int CostCategoryId { get; init; }

    public string? Title { get; init; }
    public string Code { get; init; }=string.Empty;
}

public class CreateCostGroupCommandHandler : IRequestHandler<CreateCostGroupCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCostGroupCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCostGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = new CostGroup
        {
            CostCategoryId = request.CostCategoryId,
            Title = request.Title,
            Code=request.Code
            
        };

        _context.CostGroups.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
