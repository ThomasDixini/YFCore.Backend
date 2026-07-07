using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using YFCore.Domain.Shared.ValueObjects;
using YFCore.Domain.Users.Entity;

namespace YFCore.Infraestructure.Configs.Users
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(200);
            builder.Property(c => c.City).IsRequired().HasMaxLength(200);
            builder.Property(c => c.PasswordHash).IsRequired();

            builder.OwnsOne(c => c.Email, email =>
            {
                email.Property(x => x.Value)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(320);
            });

            builder.OwnsOne(c => c.Phone, phone =>
            {
                phone.Property(x => x.Value)
                    .HasColumnName("Phone")
                    .IsRequired()
                    .HasMaxLength(32);
            });
        }
    }
}
