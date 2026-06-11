using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YFCore.Domain.Shared.Exceptions
{
    public class AmountNegativeException : DomainException
    {
        public AmountNegativeException() : base("Amount cannot be negative.")
        {
        }
    }
}
