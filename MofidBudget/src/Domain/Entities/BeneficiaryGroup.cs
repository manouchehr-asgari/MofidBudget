namespace MofidBudget.Domain.Entities;

public class BeneficiaryGroup : BaseAuditableEntity
{
    public string? Title { get; set; }
    public required string Code { get; set; }
    public IList<Beneficiary> Items { get; private set; } = new List<Beneficiary>();
}

