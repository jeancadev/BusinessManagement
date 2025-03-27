using BusinessManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Inventories.Commands
{
    public class UpdateInventoryCommandHandler
        : IRequestHandler<UpdateInventoryCommand, bool>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public UpdateInventoryCommandHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<bool> Handle(
            UpdateInventoryCommand request,
            CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(request.Id);
            if (inventory == null)
                return false;

            inventory.SetQuantity(request.Quantity);
            await _inventoryRepository.UpdateAsync(inventory);
            return true;
        }
    }
}
