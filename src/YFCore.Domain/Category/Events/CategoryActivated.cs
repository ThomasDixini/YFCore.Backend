using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.Shared.Base;

namespace YFCore.Domain.Categories.Events
{
    public class CategoryActivated : DomainEventBase
    {
        public Guid CategoryId { get; }
        public CategoryActivated(Guid categoryId) : base()
        {
            CategoryId = categoryId;
        }
    }
}