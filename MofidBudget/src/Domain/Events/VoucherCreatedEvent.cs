namespace MofidBudget.Domain.Events;

public class VoucherCreatedEvent : BaseEvent
{
    public VoucherCreatedEvent(Voucher item)
    {
        Item = item;
    }

    public Voucher Item { get; }
}
