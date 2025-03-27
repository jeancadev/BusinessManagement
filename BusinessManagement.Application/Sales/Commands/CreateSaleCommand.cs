using BusinessManagement.Application.Sales.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace BusinessManagement.Application.Sales.Commands
{
    // Retorna el Guid de la nueva venta
    public record CreateSaleCommand(
        Guid CustomerId,
        List<SaleItemDto> Items
    ) : IRequest<Guid>;
}
