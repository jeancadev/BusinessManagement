using BusinessManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Inventories.Commands
{
    public class DeleteInventoryCommandHandler
        : IRequestHandler<DeleteInventoryCommand, bool>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public DeleteInventoryCommandHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<bool> Handle(
            DeleteInventoryCommand request,
            CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(request.Id);
            if (inventory == null)
                return false;

            await _inventoryRepository.DeleteAsync(inventory);
            return true;
        }
    }
}
