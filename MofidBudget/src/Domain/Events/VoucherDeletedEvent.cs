namespace MofidBudget.Domain.Events;

public class VoucherDeletedEvent : BaseEvent
{
    public VoucherDeletedEvent(Voucher item)
    {
        Item = item;
    }

    public Voucher Item { get; }
}
