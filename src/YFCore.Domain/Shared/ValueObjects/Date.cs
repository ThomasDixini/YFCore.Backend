using System;
using System.Globalization;

namespace YFCore.Domain.Shared.ValueObjects
{
    public sealed class Date
    {
        public DateOnly Value { get; private set; }
        public string Locale { get; private set; }

        public Date(int year, int month, int day, string locale = "ISO")
        {
            this.Value = new DateOnly(year, month, day);
            this.Locale = locale ?? "ISO";
        }

        public Date(DateOnly date, string locale = "ISO")
        {
            this.Value = date;
            this.Locale = locale ?? "ISO";
        }

        public static Date Create(int year, int month, int day, string locale = "ISO")
        {
            return new Date(year, month, day, locale);
        }

        public string Format()
        {
            return Locale?.ToUpperInvariant() switch
            {
                "US" => Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                "BR" => Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                "PT-BR" => Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                "DE" => Value.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                "JP" => Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                "ISO" => Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                _ => Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            };
        }

        public override string ToString()
        {
            return Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}
