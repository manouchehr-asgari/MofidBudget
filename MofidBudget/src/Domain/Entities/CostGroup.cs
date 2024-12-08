namespace MofidBudget.Domain.Entities;

public class CostGroup : BaseAuditableEntity
{
    public string? Title { get; set; }
    public int CostCategoryId { get; set; }
    public required string Code { get; set; }
    public CostCategory CostCategory { get; set; } = null!;
}

