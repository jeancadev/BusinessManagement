using System;
using System.Collections.Generic;

namespace BusinessManagement.Application.Sales.DTOs
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }

        public List<SaleItemDto> Items { get; set; } = new();
    }
}
