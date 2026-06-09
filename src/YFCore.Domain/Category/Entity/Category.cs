using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.Categories.Events;
using YFCore.Domain.Shared.Base;

namespace YFCore.Domain.Categories.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }

        public Category(Guid id, string name, string description)
        {
            this.Validate(name, description);
            this.Id = id;
            this.Name = name.ToUpper();
            this.Description = description.ToUpper();
            this.Active = true;
        }

        private void Validate(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            if (description.Length > 200)
                throw new ArgumentException("Description cannot be longer than 200 characters.");
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

        public void Activate()
        {
            this.Active = true;
            AddDomainEvent(new CategoryActivated(this.Id));
        }

        public void Deactivate()
        {
            this.Active = false;
            AddDomainEvent(new CategoryDeactivated(this.Id));
        }
    }
}