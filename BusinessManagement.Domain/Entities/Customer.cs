using BusinessManagement.Domain.Common;
using BusinessManagement.Domain.Exceptions;
using System;

namespace BusinessManagement.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        protected Customer() { }

        public Customer(string firstName, string lastName, string email)
            : base(Guid.NewGuid())
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);
        }

        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("El nombre no puede estar vacío.");

            FirstName = firstName.Trim();
            UpdateTimestamp();
        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("El apellido no puede estar vacío.");

            LastName = lastName.Trim();
            UpdateTimestamp();
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("El email no puede estar vacío.");

            // Aquí se podria añadir validacion de formato (Regex)
            Email = email.Trim();
            UpdateTimestamp();
        }
    }
}
