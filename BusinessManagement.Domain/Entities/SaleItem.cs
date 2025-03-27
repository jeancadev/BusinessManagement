using BusinessManagement.Domain.Common;
using BusinessManagement.Domain.Exceptions;
using System;

namespace BusinessManagement.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        protected SaleItem() { }

        public SaleItem(Guid productId, int quantity, decimal unitPrice)
            : base(Guid.NewGuid())
        {
            if (quantity <= 0)
                throw new DomainException("La cantidad debe ser mayor que cero.");

            if (unitPrice < 0)
                throw new DomainException("El precio unitario no puede ser negativo.");

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal GetTotal()
        {
            return Quantity * UnitPrice;
        }
    }
}
