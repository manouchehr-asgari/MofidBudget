using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Application.BeneficiaryEmplyees.Commands.CreateBeneficiaryEmplyee;

public record CreateBeneficiaryEmplyeeCommand : IRequest<int>
{
    public int BeneficiaryId { get; init; }
    public DateTime? Date { get; set; }
    public int EmployeeCount { get; set; }
}

public class CreateBeneficiaryEmplyeeCommandHandler : IRequestHandler<CreateBeneficiaryEmplyeeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBeneficiaryEmplyeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBeneficiaryEmplyeeCommand request, CancellationToken cancellationToken)
    {
        var entity = new BeneficiaryEmplyee
        {
            BeneficiaryId = request.BeneficiaryId,
            Date = request.Date,
            EmployeeCount = request.EmployeeCount  
        };

        _context.BeneficiaryEmplyees.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

