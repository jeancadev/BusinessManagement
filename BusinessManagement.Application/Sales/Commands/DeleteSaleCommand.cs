using MediatR;
using System;

namespace BusinessManagement.Application.Sales.Commands
{
    public record DeleteSaleCommand(Guid SaleId) : IRequest<bool>;
}
