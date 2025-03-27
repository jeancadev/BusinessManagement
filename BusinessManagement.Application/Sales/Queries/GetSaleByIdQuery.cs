using BusinessManagement.Application.Sales.DTOs;
using MediatR;
using System;

namespace BusinessManagement.Application.Sales.Queries
{
    public record GetSaleByIdQuery(Guid SaleId) : IRequest<SaleDto?>;
}
