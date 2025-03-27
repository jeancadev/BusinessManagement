using MediatR;
using System;

namespace BusinessManagement.Application.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;
}
