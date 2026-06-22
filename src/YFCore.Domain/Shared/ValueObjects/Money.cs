using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace YFCore.Domain.Shared.ValueObjects
{
    public sealed class Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        public Money(decimal amount, string currency)
        {
            this.Amount = amount;
            this.Currency = currency;
        }

        public string Format()
        {
            string formattedAmount = Amount.ToString("N2", CultureInfo.InvariantCulture);
            return Currency?.ToUpperInvariant() switch
            {
                "USD" => "$" + formattedAmount,
                "EUR" => "€" + formattedAmount,
                "GBP" => "£" + formattedAmount,
                "BRL" => "R$" + formattedAmount,
                "JPY" => "¥" + Amount.ToString("N0", CultureInfo.InvariantCulture),
                "AUD" => "A$" + formattedAmount,
                "CAD" => "C$" + formattedAmount,
                _ => formattedAmount + " " + Currency,
            };
        }

        override public string ToString()
        {
            return Amount.ToString("0.00", CultureInfo.InvariantCulture) + " " + Currency;
        }
    }
}
