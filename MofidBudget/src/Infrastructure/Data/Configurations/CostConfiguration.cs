using System.Reflection.Emit;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Infrastructure.Data.Configurations;

public class CostConfiguration : IEntityTypeConfiguration<Cost>
{
    public void Configure(EntityTypeBuilder<Cost> builder)
    {
        builder.Property(t => t.RefractionLevel)
            .IsRequired();
        builder.Property(t => t.VoucherDate)
            .IsRequired();
        builder.Property(t => t.CostTypeId)
            .IsRequired();
        builder.Property(t => t.Amount)
       .IsRequired();
       

    }
}
