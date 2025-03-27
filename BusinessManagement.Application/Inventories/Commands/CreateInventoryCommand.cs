using MediatR;
using System;

namespace BusinessManagement.Application.Inventories.Commands
{
    public record CreateInventoryCommand(
        Guid ProductId,
        int Quantity
    ) : IRequest<Guid>;
}
