namespace MofidBudget.Domain.Entities;

public class CostCategory : BaseAuditableEntity
{
    public string? Title { get; set; }
    public required string Code { get; set; }
    public IList<CostGroup> Items { get; private set; } = new List<CostGroup>();
}

