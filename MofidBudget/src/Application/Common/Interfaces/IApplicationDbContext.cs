using MofidBudget.Domain.Entities;

namespace MofidBudget.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    
    DbSet<CostCategory> CostCategories { get; }
    DbSet<CostGroup> CostGroups { get; }
    DbSet<CostType> CostTypes { get; }
    DbSet<MofidBudget.Domain.Entities.BeneficiaryType> BeneficiaryTypes { get; }
    DbSet<Beneficiary> Beneficiaries { get; }
    DbSet<BeneficiaryGroup> BeneficiaryGroups { get; }
    DbSet<BeneficiaryRelationShip> BeneficiaryRelationShips { get; }
    DbSet<Voucher> Vouchers { get; }
    DbSet<Cost> Costs { get; }
    DbSet<BeneficiaryEmplyee> BeneficiaryEmplyees { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
