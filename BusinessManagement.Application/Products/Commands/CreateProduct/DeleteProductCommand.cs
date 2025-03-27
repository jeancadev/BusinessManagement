using MediatR;
using System;

namespace BusinessManagement.Application.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<bool>;
}
