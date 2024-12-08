using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Entities;
using MofidBudget.Domain.Enums;
using MofidBudget.Domain.Events;

namespace MofidBudget.Application.Vouchers.Commands.CreateVoucher;

public record CreateVoucherCommand : IRequest<int>
{
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

public class CreateVoucherCommandHandler : IRequestHandler<CreateVoucherCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateVoucherCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateVoucherCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new Voucher
            {
                CostTypeId = request.CostTypeId,
                BeneficiaryId = request.BeneficiaryId,
                AccountTitle = request.AccountTitle,
                AccountCode = request.AccountCode,
                CompanyName = request.CompanyName,
                Amount = request.Amount,
                VoucherDate = request.VoucherDate,
                VoucherNumber = request.VoucherNumber,
                Description = request.Description
            };

            _context.Vouchers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            entity.AddDomainEvent(new VoucherCreatedEvent(entity));
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            throw;
        }
        
    }
}

