using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using YFCore.Domain.ProductEntity;

namespace YFCore.Infraestructure.Configs.Products
{
    public class ProductsConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CategoryId).IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(500);
            builder.Property(c => c.Active).IsRequired();
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