using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Infrastructure.Data.Configurations;

public class BeneficiaryGroupConfiguration : IEntityTypeConfiguration<BeneficiaryGroup>
{
    public void Configure(EntityTypeBuilder<BeneficiaryGroup> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.Code)
            .HasMaxLength(10)
            .IsUnicode()
            .IsRequired();
    }
}
