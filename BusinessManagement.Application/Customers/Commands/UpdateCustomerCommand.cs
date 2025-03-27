using MediatR;
using System;

namespace BusinessManagement.Application.Customers.Commands
{
    public record UpdateCustomerCommand(
        Guid Id,
        string FirstName,
        string LastName,
        string Email
    ) : IRequest<bool>;
}
