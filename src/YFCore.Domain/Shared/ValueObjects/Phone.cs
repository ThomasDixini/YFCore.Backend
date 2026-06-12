using System;
using System.Text;

namespace YFCore.Domain.Shared.ValueObjects
{
    public sealed class Phone
    {
        public string Value { get; private set; }

        public Phone(string value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            var normalized = Normalize(value);
            if (string.IsNullOrWhiteSpace(normalized))
                throw new ArgumentException("Phone cannot be empty.", nameof(value));

            if (!IsValidPhone(normalized))
                throw new ArgumentException("Phone is not valid.", nameof(value));

            this.Value = normalized;
        }

        private static string Normalize(string value)
        {
            var trimmed = value.Trim();
            if (trimmed.Length == 0)
                return string.Empty;

            var builder = new StringBuilder();
            foreach (var character in trimmed)
            {
                if (char.IsDigit(character))
                {
                    builder.Append(character);
                }
                else if (character == '+' && builder.Length == 0)
                {
                    builder.Append(character);
                }
                else if (character is ' ' or '-' or '(' or ')' or '.')
                {
                    continue;
                }
                else
                {
                    return string.Empty;
                }
            }

            return builder.ToString();
        }

        private static bool IsValidPhone(string normalized)
        {
            if (normalized.StartsWith('+'))
            {
                var digits = normalized[1..];
                return digits.Length >= 7 && digits.Length <= 15;
            }

            return normalized.Length >= 7 && normalized.Length <= 15;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}