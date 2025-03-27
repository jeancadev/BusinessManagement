using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Application.Inventories.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Inventories.Queries
{
    public class GetInventoryByIdQueryHandler
        : IRequestHandler<GetInventoryByIdQuery, InventoryDto?>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public GetInventoryByIdQueryHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<InventoryDto?> Handle(
            GetInventoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(request.Id);
            if (inventory == null)
                return null;

            return new InventoryDto
            {
                Id = inventory.Id,
                ProductId = inventory.ProductId,
                Quantity = inventory.Quantity
            };
        }
    }
}
