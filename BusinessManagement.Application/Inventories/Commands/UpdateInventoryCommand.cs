using MediatR;
using System;

namespace BusinessManagement.Application.Inventories.Commands
{
    public record UpdateInventoryCommand(
        Guid Id,
        int Quantity
    ) : IRequest<bool>;
}
