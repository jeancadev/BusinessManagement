using BusinessManagement.Domain.Common;
using BusinessManagement.Domain.Exceptions;
using System;

namespace BusinessManagement.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        protected Inventory() { }

        public Inventory(Guid productId, int quantity)
            : base(Guid.NewGuid())
        {
            SetProductId(productId);
            SetQuantity(quantity);
        }

        public void SetProductId(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new DomainException("El ProductId no puede ser vacío.");
            ProductId = productId;
            UpdateTimestamp();
        }

        public void SetQuantity(int quantity)
        {
            if (quantity < 0)
                throw new DomainException("La cantidad de inventario no puede ser negativa.");
            Quantity = quantity;
            UpdateTimestamp();
        }
    }
}
