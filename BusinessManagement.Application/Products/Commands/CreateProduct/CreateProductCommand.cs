using MediatR;
using System;

namespace BusinessManagement.Application.Products.Commands
{
    public record CreateProductCommand(
        string Name,
        string Description,
        decimal Price,
        int Stock
    ) : IRequest<Guid>;
}
