using MediatR;

namespace BusinessManagement.Application.Customers.Commands
{
    // Retorna un Guid que será el ID del nuevo cliente
    public record CreateCustomerCommand(
        string FirstName,
        string LastName,
        string Email
    ) : IRequest<Guid>;
}
