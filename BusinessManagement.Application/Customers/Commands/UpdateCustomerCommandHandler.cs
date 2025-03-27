using BusinessManagement.Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Customers.Commands
{
    public class UpdateCustomerCommandHandler
        : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(
            UpdateCustomerCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Se obtiene el cliente de la BD
            var existingCustomer = await _customerRepository.GetByIdAsync(request.Id);
            if (existingCustomer == null)
            {
                return false;
            }

            // 2. Actualiza los datos usando métodos del dominio
            existingCustomer.SetFirstName(request.FirstName);
            existingCustomer.SetLastName(request.LastName);
            existingCustomer.SetEmail(request.Email);

            // 3. Guarda cambios
            await _customerRepository.UpdateAsync(existingCustomer);
            return true;
        }
    }
}
