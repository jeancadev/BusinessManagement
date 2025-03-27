using System;

namespace BusinessManagement.Application.Inventories.DTOs
{
    public class InventoryDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
