using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Crear la entidad con validaciones del dominio
            var product = new Product(
                name: request.Name,
                description: request.Description,
                price: request.Price,
                stock: request.Stock
            );

            // 2. Guardar en la base de datos (repositorio)
            await _productRepository.AddAsync(product);

            // 3. Retornar el Id generado
            return product.Id;
        }
    }
}
