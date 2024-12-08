using System.Reflection;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MofidBudget.Application.Common.Interfaces;
using MofidBudget.Domain.Entities;
using MofidBudget.Infrastructure.Identity;

namespace MofidBudget.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<CostCategory> CostCategories => Set<CostCategory>();
    public DbSet<CostGroup> CostGroups => Set<CostGroup>();
    public DbSet<CostType> CostTypes => Set<CostType>();
    public DbSet<BeneficiaryType> BeneficiaryTypes => Set<BeneficiaryType>();
    public DbSet<Beneficiary> Beneficiaries => Set<Beneficiary>();
    public DbSet<BeneficiaryRelationShip> BeneficiaryRelationShips => Set<BeneficiaryRelationShip>();
    public DbSet<BeneficiaryGroup> BeneficiaryGroups => Set<BeneficiaryGroup>();
    public DbSet<Voucher> Vouchers => Set<Voucher>();
    public DbSet<Cost> Costs => Set<Cost>();
    public DbSet<BeneficiaryEmplyee> BeneficiaryEmplyees => Set<BeneficiaryEmplyee>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<BeneficiaryRelationShip>()
    .HasOne(e => e.FromBeneficiary)
    .WithMany().OnDelete(DeleteBehavior.NoAction);

        builder.Entity<BeneficiaryRelationShip>()
            .HasOne(e => e.ToBeneficiary)
            .WithMany().OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Cost>()
          .HasOne(e => e.Beneficiary)
          .WithMany().OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Cost>()
         .HasOne(e => e.Voucher)
         .WithMany().OnDelete(DeleteBehavior.NoAction);
        builder.Entity<BeneficiaryEmplyee>()
        .HasOne(e => e.Beneficiary)
        .WithMany().OnDelete(DeleteBehavior.NoAction);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}
