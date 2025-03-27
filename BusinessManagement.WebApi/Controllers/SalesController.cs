using BusinessManagement.Application.Sales.Commands;
using BusinessManagement.Application.Sales.Queries;
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
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
        {
            var newSaleId = await _mediator.Send(command);
            return Ok(newSaleId);
        }

        [HttpPost("{saleId:guid}/items")]
        public async Task<IActionResult> AddItemToSale(
            Guid saleId,
            [FromBody] AddItemToSaleCommand command)
        {
            // Forza que el SaleId del comando coincida con el de la URL
            if (saleId != command.SaleId)
                return BadRequest("El SaleId de la ruta y el del comando no coinciden.");

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound($"No se encontró la venta con Id: {saleId}");

            return NoContent();
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetSalesPaged(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null)
        {
            var query = new GetAllSalesPagedQuery(pageNumber, pageSize, searchTerm);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _mediator.Send(new GetAllSalesQuery());
            return Ok(sales);
        }

        [HttpGet("{saleId:guid}")]
        public async Task<IActionResult> GetSaleById(Guid saleId)
        {
            var sale = await _mediator.Send(new GetSaleByIdQuery(saleId));
            if (sale == null)
                return NotFound($"No se encontró la venta con Id: {saleId}");

            return Ok(sale);
        }

        [HttpDelete("{saleId:guid}")]
        public async Task<IActionResult> DeleteSale(Guid saleId)
        {
            var deleted = await _mediator.Send(new DeleteSaleCommand(saleId));
            if (!deleted)
                return NotFound($"No se encontró la venta con Id: {saleId}");

            return NoContent();
        }
    }
}
