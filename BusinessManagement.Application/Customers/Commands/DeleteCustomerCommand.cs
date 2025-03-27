using MediatR;
using System;

namespace BusinessManagement.Application.Customers.Commands
{
    public record DeleteCustomerCommand(Guid Id) : IRequest<bool>;
}
