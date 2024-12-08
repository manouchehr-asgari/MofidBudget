using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Infrastructure.Data.Configurations;

public class CostGroupConfiguration : IEntityTypeConfiguration<CostGroup>
{
    public void Configure(EntityTypeBuilder<CostGroup> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.Code)
            .HasMaxLength(10)
            .IsRequired();
    }
}
