namespace MofidBudget.Domain.Entities;

public class BeneficiaryType : BaseAuditableEntity
{
    public string? Title { get; set; }
    public IList<Beneficiary> Items { get; private set; } = new List<Beneficiary>();
}

