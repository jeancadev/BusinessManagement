using BusinessManagement.Application.Customers.Commands;
using BusinessManagement.Application.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace BusinessManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requiere token JWT válido para cualquier método de este controlador
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Crear cliente
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(
            [FromBody] CreateCustomerCommand command)
        {
            var newCustomerId = await _mediator.Send(command);
            return Ok(newCustomerId);
        }

        // Obtener todos los clientes
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(customers);
        }

        // Obtener cliente por Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (customer == null)
                return NotFound($"No se encontró el cliente con Id: {id}");

            return Ok(customer);
        }

        // Actualizar cliente
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCustomer(
            Guid id,
            [FromBody] UpdateCustomerCommand command)
        {
            // Se forza que el Id del comando coincida con el de la ruta
            if (id != command.Id)
                return BadRequest("El Id de la ruta y el del comando no coinciden.");

            var updated = await _mediator.Send(command);
            if (!updated)
                return NotFound($"No se encontró el cliente con Id: {id}");

            return NoContent();
        }

        // Eliminar cliente
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var deleted = await _mediator.Send(new DeleteCustomerCommand(id));
            if (!deleted)
                return NotFound($"No se encontró el cliente con Id: {id}");

            return NoContent();
        }
    }
}
