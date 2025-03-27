using MediatR;
using System;

namespace BusinessManagement.Application.Sales.Commands
{
    public record AddItemToSaleCommand(
        Guid SaleId,
        Guid ProductId,
        int Quantity,
        decimal UnitPrice
    ) : IRequest<bool>;
}
