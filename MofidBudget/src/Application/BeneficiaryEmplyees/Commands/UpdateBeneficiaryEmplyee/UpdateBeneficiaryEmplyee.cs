using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.BeneficiaryEmplyees.Commands.UpdateBeneficiaryEmplyee;
public record UpdateBeneficiaryEmplyeeCommand : IRequest
{
    public int Id { get; init; }

    public int BeneficiaryId { get; init; }
    public DateTime? Date { get; set; }
    public int EmployeeCount { get; set; }

}

public class UpdateBeneficiaryEmplyeeCommandHandler : IRequestHandler<UpdateBeneficiaryEmplyeeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBeneficiaryEmplyeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBeneficiaryEmplyeeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BeneficiaryEmplyees
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        entity.BeneficiaryId = request.BeneficiaryId;
        entity.Date = request.Date;
        entity.EmployeeCount = request.EmployeeCount;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

