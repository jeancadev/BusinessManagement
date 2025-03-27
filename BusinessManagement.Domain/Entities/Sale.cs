using BusinessManagement.Domain.Common;
using BusinessManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessManagement.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Guid CustomerId { get; private set; }
        public DateTime SaleDate { get; private set; }
        public decimal TotalAmount { get; private set; }

        private readonly List<SaleItem> _items = new List<SaleItem>();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        protected Sale() { }

        public Sale(Guid customerId, IEnumerable<SaleItem> items)
            : base(Guid.NewGuid())
        {
            if (items == null || !items.Any())
                throw new DomainException("La venta debe tener al menos un ítem.");

            CustomerId = customerId;
            SaleDate = DateTime.UtcNow;

            // Agrego ítems mediante un método para garantizar validaciones
            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        public void AddItem(SaleItem item)
        {
            if (item == null)
                throw new DomainException("No se puede agregar un ítem nulo.");

            _items.Add(item);
            CalculateTotal();
            UpdateTimestamp();
        }

        public void RemoveItem(SaleItem item)
        {
            if (item == null)
                throw new DomainException("No se puede remover un ítem nulo.");

            _items.Remove(item);
            CalculateTotal();
            UpdateTimestamp();
        }

        private void CalculateTotal()
        {
            TotalAmount = _items.Sum(i => i.GetTotal());
        }
    }
}
