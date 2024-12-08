using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Infrastructure.Data.Configurations;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.Property(t => t.CostTypeId)
            .IsRequired();
        builder.Property(t => t.BeneficiaryId)
            .IsRequired();
        builder.Property(t => t.Amount)
            .IsRequired();
        builder.Property(t => t.VoucherDate)
           .IsRequired();
        builder.Property(t => t.Description)
            .HasMaxLength(1000);
        builder.Property(t => t.CompanyName)
           .HasMaxLength(200);
        builder.Property(t => t.AccountCode)
          .HasMaxLength(200);
        builder.Property(t => t.AccountTitle)
          .HasMaxLength(200);

    }

}
