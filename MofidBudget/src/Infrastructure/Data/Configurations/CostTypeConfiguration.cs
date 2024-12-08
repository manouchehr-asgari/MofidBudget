using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Infrastructure.Data.Configurations;

public class CostTypeConfiguration : IEntityTypeConfiguration<CostType>
{
    public void Configure(EntityTypeBuilder<CostType> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.Code)
            .HasMaxLength(10)
            .IsRequired();
    }
}
