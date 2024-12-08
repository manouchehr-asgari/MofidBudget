using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MofidBudget.Application.Common.Interfaces;

namespace MofidBudget.Application.Vouchers.Commands.DeleteVoucher;


public record DeleteVoucherCommand(int Id) : IRequest;

public class DeleteVoucherCommandHandler : IRequestHandler<DeleteVoucherCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteVoucherCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteVoucherCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Vouchers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Vouchers.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
