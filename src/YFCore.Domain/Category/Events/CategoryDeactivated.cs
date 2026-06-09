using System;

using YFCore.Domain.Shared.Base;

namespace YFCore.Domain.Categories.Events
{
    public class CategoryDeactivated : DomainEventBase
    {
        public Guid CategoryId { get; }

        public CategoryDeactivated(Guid categoryId) : base()
        {
            CategoryId = categoryId;
        }
    }
}
