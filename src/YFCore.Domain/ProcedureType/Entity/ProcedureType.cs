using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.Shared.Base;
using YFCore.Domain.Shared.Helpers;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Domain.ProcedureTypes.Entity
{
    public class ProcedureType : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }

        public ProcedureType(string name, string description)
        {
            var price = new Money(0, "USD");
            this.Validate(name, description, price);
            this.Name = name.ToUpper();
            this.Description = description.ToUpper();
            this.Price = price;
        }

        public void Validate(string name, string description, Money price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            ArgumentNullException.ThrowIfNull(description, nameof(description));
            if (description.Length > 200)
                throw new ArgumentException("Description cannot be longer than 200 characters.");
            ArgumentNullException.ThrowIfNull(price.Currency, nameof(price.Currency));
            if (price.Amount < 0)
                throw new ArgumentException("Price cannot be negative.");
        }

        public void ChangeName(string name)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            this.Name = name.ToUpper();
        }

        public void ChangeDescription(string description)
        {
            ArgumentNullException.ThrowIfNull(description, nameof(description));
            if (description.Length > 200)
                throw new ArgumentException("Description cannot be longer than 200 characters.");
            this.Description = description.ToUpper();
        }

        public void ChangePrice(Money price)
        {
            ArgumentNullException.ThrowIfNull(price, nameof(price));
            ArgumentNullException.ThrowIfNull(price.Currency, nameof(price.Currency));
            if (price.Amount < 0)
                throw new ArgumentException("Price cannot be negative.");
            this.Price = price;
        }
    }
}
