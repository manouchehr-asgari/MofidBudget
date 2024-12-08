using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Enums;

namespace MofidBudget.Application.Vouchers.Commands.UpdateVoucher;
public record UpdateVoucherCommand : IRequest
{
    public int Id { get; set; }
    public int CostTypeId { get; set; }
    public DateTime? VoucherDate { get; set; }
    public int BeneficiaryId { get; set; }
    public float Amount { get; set; }
    public string? Description { get; set; }
    public string? CompanyName { get; set; }
    public string? AccountTitle { get; set; }
    public string? AccountCode { get; set; }
    public int VoucherNumber { get; set; }
    public Location Location { get; set; }

}

public class UpdateVoucherCommandHandler : IRequestHandler<UpdateVoucherCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateVoucherCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateVoucherCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Vouchers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        entity.CostTypeId = request.CostTypeId;
        entity.BeneficiaryId = request.BeneficiaryId;
        entity.AccountTitle = request.AccountTitle;
        entity.AccountCode = request.AccountCode;
        entity.CompanyName = request.CompanyName;
        entity.Amount = request.Amount;
        entity.VoucherDate = request.VoucherDate;
        entity.VoucherNumber = request.VoucherNumber;
        entity.Description = request.Description;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

