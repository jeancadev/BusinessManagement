using MediatR;
using System;

namespace BusinessManagement.Application.Inventories.Commands
{
    public record DeleteInventoryCommand(Guid Id) : IRequest<bool>;
}
