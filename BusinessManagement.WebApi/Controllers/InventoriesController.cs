using BusinessManagement.Application.Inventories.Commands;
using BusinessManagement.Application.Inventories.Queries;
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
    public class InventoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory(
            [FromBody] CreateInventoryCommand command)
        {
            var newInventoryId = await _mediator.Send(command);
            return Ok(newInventoryId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventories()
        {
            var inventories = await _mediator.Send(new GetAllInventoriesQuery());
            return Ok(inventories);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetInventoryById(Guid id)
        {
            var inventory = await _mediator.Send(new GetInventoryByIdQuery(id));
            if (inventory == null)
                return NotFound($"No se encontró el registro de inventario con Id: {id}");

            return Ok(inventory);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateInventory(Guid id,
            [FromBody] UpdateInventoryCommand command)
        {
            if (id != command.Id)
                return BadRequest("El Id de la ruta y el del comando no coinciden.");

            var updated = await _mediator.Send(command);
            if (!updated)
                return NotFound($"No se encontró el registro de inventario con Id: {id}");

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteInventory(Guid id)
        {
            var deleted = await _mediator.Send(new DeleteInventoryCommand(id));
            if (!deleted)
                return NotFound($"No se encontró el registro de inventario con Id: {id}");

            return NoContent();
        }
    }
}
