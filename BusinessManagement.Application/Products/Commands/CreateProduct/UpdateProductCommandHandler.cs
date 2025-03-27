using BusinessManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Products.Commands
{
    public class UpdateProductCommandHandler
        : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(
            UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
                return false;

            product.SetName(request.Name);
            product.SetDescription(request.Description);
            product.SetPrice(request.Price);
            product.SetStock(request.Stock);

            await _productRepository.UpdateAsync(product);
            return true;
        }
    }
}
