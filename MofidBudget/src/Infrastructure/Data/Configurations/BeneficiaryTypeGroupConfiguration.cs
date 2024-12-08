using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Infrastructure.Data.Configurations;

public class BeneficiaryTypeGroupConfiguration : IEntityTypeConfiguration<BeneficiaryType>
{
    public void Configure(EntityTypeBuilder<BeneficiaryType> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
            
    }
}
