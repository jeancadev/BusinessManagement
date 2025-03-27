using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Application.Inventories.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Inventories.Queries
{
    public class GetAllInventoriesQueryHandler
        : IRequestHandler<GetAllInventoriesQuery, List<InventoryDto>>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public GetAllInventoriesQueryHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<List<InventoryDto>> Handle(
            GetAllInventoriesQuery request,
            CancellationToken cancellationToken)
        {
            var inventories = await _inventoryRepository.GetAllAsync();
            return inventories.Select(i => new InventoryDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList();
        }
    }
}
