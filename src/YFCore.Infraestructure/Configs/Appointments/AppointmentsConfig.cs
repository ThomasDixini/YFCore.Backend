using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using YFCore.Domain.Appointments.Entity;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Infraestructure.Configs.Appointments
{
    public class AppointmentsConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Status).HasConversion<string>().IsRequired();
            builder.Property(c => c.ProcedureTypeId).IsRequired();
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.Token).IsRequired().HasMaxLength(100);

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

            builder.Property(c => c.TimeSlots)
                .HasConversion(new ValueConverter<UnavailableTimeSlots, string>(
                    ts => ConvertUnavailableTimeSlotsToString(ts),
                    value => ConvertToUnavailableTimeSlots(value)
                ))
                .HasColumnName("TimeSlots")
                .IsRequired();
        }

        private static string ConvertUnavailableTimeSlotsToString(UnavailableTimeSlots ts)
        {
            return string.Join("|", ts.Date.Value.ToString("yyyy-MM-dd"), string.Join(",", ts.TimeSlots.Select(t => t.ToString("HH:mm"))));
        }

        private static UnavailableTimeSlots ConvertToUnavailableTimeSlots(string value)
        {
            var parts = value.Split('|', 2);
            var date = new Date(DateOnly.Parse(parts[0]));
            var times = parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1])
                ? parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(TimeOnly.Parse)
                : Enumerable.Empty<TimeOnly>();
            return new UnavailableTimeSlots(date, times);
        }
    }
}
