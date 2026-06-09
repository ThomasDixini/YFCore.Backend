using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YFCore.Domain.Shared.Exceptions
{
    public class ProductPriceNegativeException : DomainException
    {
        public ProductPriceNegativeException() : base("Product price cannot be negative.")
        {
        }
    }
}