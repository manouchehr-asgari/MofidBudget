namespace MofidBudget.Domain.Entities;

public class CostType : BaseAuditableEntity
{
    public string? Title { get; set; }
    public required string Code { get; set; }
    public int CostGroupId { get; set; }
    public CostGroup CostGroup { get; set; } = null!;
}

