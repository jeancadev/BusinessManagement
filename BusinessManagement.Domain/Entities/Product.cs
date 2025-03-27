using BusinessManagement.Domain.Common;
using BusinessManagement.Domain.Exceptions;
using System;

namespace BusinessManagement.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        // Constructor protegido para EF
        protected Product() { }

        // Constructor para crear un producto nuevo
        public Product(string name, string description, decimal price, int stock)
            : base(Guid.NewGuid())
        {
            SetName(name);
            SetDescription(description);
            SetPrice(price);
            SetStock(stock);
        }

        // Métodos de modificación con validaciones
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("El nombre del producto no puede estar vacío.");

            Name = name;
            UpdateTimestamp();
        }

        public void SetDescription(string description)
        {
            Description = description?.Trim() ?? string.Empty;
            UpdateTimestamp();
        }

        public void SetPrice(decimal price)
        {
            if (price < 0)
                throw new DomainException("El precio no puede ser negativo.");

            Price = price;
            UpdateTimestamp();
        }

        public void SetStock(int stock)
        {
            if (stock < 0)
                throw new DomainException("El stock no puede ser negativo.");

            Stock = stock;
            UpdateTimestamp();
        }
    }
}
