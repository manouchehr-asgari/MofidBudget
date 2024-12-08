using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Entities;
using MofidBudget.Domain.Enums;

namespace MofidBudget.Application.Beneficiaries.Commands.CreateBeneficiary;

public record CreateBeneficiaryCommand : IRequest<int>
{
    public int BeneficiaryGroupId { get; init; }
    public int BeneficiaryTypeId { get; init; }
    public Location Location { get; set; }
    public string Code { get; init; } = string.Empty;
    public string? Title { get; init; }
}

public class CreateBeneficiaryCommandHandler : IRequestHandler<CreateBeneficiaryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBeneficiaryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Beneficiary
        {
            BeneficiaryGroupId = request.BeneficiaryGroupId,
            BeneficiaryTypeId = request.BeneficiaryTypeId,
            Location = request.Location,
            Title = request.Title,
            Code= request.Code  
        };

        _context.Beneficiaries.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

