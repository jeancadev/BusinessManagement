using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Sales.Commands
{
    public class AddItemToSaleCommandHandler
        : IRequestHandler<AddItemToSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;

        public AddItemToSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<bool> Handle(
            AddItemToSaleCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Obtiene la venta de la BD
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);
            if (sale == null)
                return false;

            // 2. Crea el SaleItem y lo agregamos
            var newItem = new SaleItem(request.ProductId, request.Quantity, request.UnitPrice);
            sale.AddItem(newItem);

            // 3. Guarda cambios
            await _saleRepository.UpdateAsync(sale);
            return true;
        }
    }
}
