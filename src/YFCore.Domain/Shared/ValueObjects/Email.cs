using System;
using System.Net.Mail;

namespace YFCore.Domain.Shared.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; private set; }

        public Email(string value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            var trimmedValue = value.Trim();
            if (string.IsNullOrWhiteSpace(trimmedValue))
                throw new ArgumentException("Email cannot be empty.", nameof(value));

            if (!IsValidEmail(trimmedValue))
                throw new ArgumentException("Email is not valid.", nameof(value));

            this.Value = trimmedValue;
        }

        private static bool IsValidEmail(string value)
        {
            try
            {
                var address = new MailAddress(value);
                return address.Address.Equals(value, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}