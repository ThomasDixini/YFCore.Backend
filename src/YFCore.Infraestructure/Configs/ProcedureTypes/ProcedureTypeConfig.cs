using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using YFCore.Domain.ProcedureTypes.Entity;

namespace YFCore.Infraestructure.Configs.ProcedureTypes
{
    public class ProcedureTypeConfig : IEntityTypeConfiguration<ProcedureType>
    {
        public void Configure(EntityTypeBuilder<ProcedureType> builder)
        {
            builder.ToTable("ProcedureTypes");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(200);
            builder.OwnsOne(c => c.Price, price =>
            {
                price.Property(x => x.Amount)
                    .HasColumnName("PriceAmount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                price.Property(x => x.Currency)
                    .HasColumnName("PriceCurrency")
                    .HasMaxLength(3)
                    .IsRequired();
            });
        }
    }
}
