using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Customers.Commands
{
    public class CreateCustomerCommandHandler
        : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Guid> Handle(
            CreateCustomerCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Se crea la entidad de dominio con validaciones en el constructor o métodos "Set..."
            var newCustomer = new Customer(
                request.FirstName,
                request.LastName,
                request.Email
            );

            // 2. Guarda en la BD
            await _customerRepository.AddAsync(newCustomer);

            // 3. Retorna el Id generado
            return newCustomer.Id;
        }
    }
}
