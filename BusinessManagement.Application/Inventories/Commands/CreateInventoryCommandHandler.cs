using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Inventories.Commands
{
    public class CreateInventoryCommandHandler
        : IRequestHandler<CreateInventoryCommand, Guid>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public CreateInventoryCommandHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Guid> Handle(
            CreateInventoryCommand request,
            CancellationToken cancellationToken)
        {
            var inventory = new Inventory(request.ProductId, request.Quantity);
            await _inventoryRepository.AddAsync(inventory);
            return inventory.Id;
        }
    }
}
