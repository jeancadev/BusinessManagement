using BusinessManagement.Application.Products.Commands;
using BusinessManagement.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace BusinessManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return Ok(productId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetAllProductsPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool sortDesc = false)
        {
            var query = new GetAllProductsQuery(pageNumber, pageSize, searchTerm, sortBy, sortDesc);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null)
                return NotFound($"No se encontró el producto con Id: {id}");

            return Ok(product);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id,
            [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest("El Id de la ruta y el del comando no coinciden.");

            var updated = await _mediator.Send(command);
            if (!updated)
                return NotFound($"No se encontró el producto con Id: {id}");

            return NoContent();
        }

        // Este metodo requiere un token valido con rol "Admin"
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var deleted = await _mediator.Send(new DeleteProductCommand(id));
            if (!deleted)
                return NotFound($"No se encontró el producto con Id: {id}");

            // Solo un token con ClaimTypes.Role = "Admin" entra aquí
            return Ok($"Producto {id} eliminado");
        }
    }
}
