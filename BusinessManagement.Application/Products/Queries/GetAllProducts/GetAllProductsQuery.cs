using BusinessManagement.Application.Common.Models;
using MediatR;
using System.Collections.Generic;

namespace BusinessManagement.Application.Products.Queries
{
    public record GetAllProductsQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null,
    string? SortBy = null, // e.g. "Name" or "Price"
    bool SortDesc = false
) : IRequest<PaginatedResult<ProductDto>>;

}
