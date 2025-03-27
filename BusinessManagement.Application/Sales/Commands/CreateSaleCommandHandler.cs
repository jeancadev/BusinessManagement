using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Application.Sales.DTOs;
using BusinessManagement.Domain.Entities;
using BusinessManagement.Domain.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Sales.Commands
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public CreateSaleCommandHandler(
            ISaleRepository saleRepository,
            IInventoryRepository inventoryRepository)
        {
            _saleRepository = saleRepository;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            // 1. Construye la lista de SaleItem desde los DTOs
            var saleItems = request.Items.Select(i =>
                new SaleItem(i.ProductId, i.Quantity, i.UnitPrice)
            ).ToList();

            // 2. Verificar y descontar stock
            foreach (var item in saleItems)
            {
                // Obtener el registro de inventario para ese producto
                var inventory = await _inventoryRepository.GetByProductIdAsync(item.ProductId);
                if (inventory == null)
                    throw new DomainException($"No existe inventario para el producto {item.ProductId}");

                if (inventory.Quantity < item.Quantity)
                    throw new DomainException($"Stock insuficiente para el producto {item.ProductId}");

                // Descontar la cantidad vendida
                inventory.SetQuantity(inventory.Quantity - item.Quantity);
                await _inventoryRepository.UpdateAsync(inventory);
            }

            // 3. Crear la entidad de venta
            var newSale = new Sale(request.CustomerId, saleItems);

            // 4. Guardar la venta
            await _saleRepository.AddAsync(newSale);

            // 5. Retornar el Id de la nueva venta
            return newSale.Id;
        }
    }
}
