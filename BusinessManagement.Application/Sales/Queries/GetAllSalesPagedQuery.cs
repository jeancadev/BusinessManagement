using BusinessManagement.Application.Common.Models;
using BusinessManagement.Application.Sales.DTOs;
using MediatR;

namespace BusinessManagement.Application.Sales.Queries
{
    public record GetAllSalesPagedQuery(
        int PageNumber = 1,
        int PageSize = 10,
        string? SearchTerm = null
    ) : IRequest<PaginatedResult<SaleDto>>;
}
