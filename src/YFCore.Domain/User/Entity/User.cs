using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.Shared.Base;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Domain.Users.Entity
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string City { get; private set; }
        public Phone Phone { get; private set; }
        public Email Email { get; private set; }

        public User(string name, string lastName, Phone phone, Email email, string city)
        {
            this.Validate(name, lastName, phone, email, city);
            this.Name = name.ToUpper();
            this.LastName = lastName.ToUpper();
            this.Phone = phone;
            this.Email = email;
            this.City = city.ToUpper();
        }

        public void Validate(string name, string lastName, Phone phone, Email email, string city)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            if (phone == null)
                throw new ArgumentNullException(nameof(phone), "Phone cannot be null.");
            if (email == null)
                throw new ArgumentNullException(nameof(email), "Email cannot be null.");
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty.", nameof(city));
        }

        public void UpdateName(string name, string lastName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            this.Name = name.ToUpper();
            this.LastName = lastName.ToUpper();
        }

        public void UpdateContactInfo(Phone phone, Email email)
        {
            if (phone == null)
                throw new ArgumentNullException(nameof(phone), "Phone cannot be null.");
            if (email == null)
                throw new ArgumentNullException(nameof(email), "Email cannot be null.");
            this.Phone = phone;
            this.Email = email;
        }

        public void UpdateCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty.", nameof(city));
            this.City = city.ToUpper();
        }
    }
}
