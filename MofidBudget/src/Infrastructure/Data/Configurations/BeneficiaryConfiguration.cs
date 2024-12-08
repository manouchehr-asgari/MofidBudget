using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Infrastructure.Data.Configurations;

public class BeneficiaryConfiguration : IEntityTypeConfiguration<Beneficiary>
{
    public void Configure(EntityTypeBuilder<Beneficiary> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.Code)
            .HasMaxLength(10)
            .IsUnicode()
            .IsRequired();
        builder.Property(t => t.Location)
            .IsRequired();
        builder.Property(t => t.BeneficiaryGroupId)
            .IsRequired();
        builder.Property(t => t.BeneficiaryTypeId)
           .IsRequired();

    }
}
