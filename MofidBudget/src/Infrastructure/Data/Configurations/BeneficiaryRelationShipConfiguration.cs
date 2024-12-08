using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Infrastructure.Data.Configurations;

public class BeneficiaryRelationShipConfiguration : IEntityTypeConfiguration<BeneficiaryRelationShip>
{
    public void Configure(EntityTypeBuilder<BeneficiaryRelationShip> builder)
    {
        builder.Property(t => t.RefractionLevel)
            .IsRequired();
        builder.Property(t => t.FromBeneficiaryId)
            .IsRequired();
        builder.Property(t => t.RefractionType)
            .IsRequired();
        builder.Property(t => t.RefractionLevel)
            .IsRequired();

    }
}
