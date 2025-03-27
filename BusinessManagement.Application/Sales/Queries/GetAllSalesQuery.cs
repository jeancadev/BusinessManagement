using BusinessManagement.Application.Sales.DTOs;
using MediatR;
using System.Collections.Generic;

namespace BusinessManagement.Application.Sales.Queries
{
    public record GetAllSalesQuery() : IRequest<List<SaleDto>>;
}
