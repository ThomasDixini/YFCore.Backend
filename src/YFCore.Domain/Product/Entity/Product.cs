using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.ProductValueObjects;
using YFCore.Domain.Shared.Exceptions;

namespace YFCore.Domain.ProductEntity
{
    public class Product
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public bool Active { get; private set; }
        public Guid CategoryId { get; private set; }

        public Product(string id, string name, string description, Money price, Guid categoryId)
        {
            this.Validate(name, description, price, categoryId);
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.CategoryId = categoryId;
        }

        public void Validate(string name, string description, Money price, Guid categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            if (description.Length > 200)
                throw new ArgumentException("Description cannot be longer than 200 characters.");
            ArgumentNullException.ThrowIfNull(price.Currency, nameof(price.Currency));
            if (price.Amount < 0)
                throw new ProductPriceNegativeException();
            if (categoryId == Guid.Empty)
                throw new ArgumentException("CategoryId cannot be empty.");
        }

        public void ChangeName(string name)
        {
            this.Name = name.ToUpper();
        }

        public void ChangeDescription(string description)
        {
            if (description.Length > 200)
                throw new ArgumentException("Description cannot be longer than 200 characters.");
            this.Description = description.ToUpper();
        }

        public void ChangePrice(Money price)
        {
            ArgumentNullException.ThrowIfNull(price.Currency, nameof(price.Currency));
            if (price.Amount < 0)
                throw new ProductPriceNegativeException();

            this.Price = price;
        }

        public void Activate()
        {
            this.Active = true;
        }

        public void Deactivate()
        {
            this.Active = false;
        }
    }
}