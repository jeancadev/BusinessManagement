using AutoMapper;
using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Products.Queries
{
    public class GetAllProductsQueryHandler
    : IRequestHandler<GetAllProductsQuery, PaginatedResult<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ProductDto>> Handle(
            GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            // Llama al metodo paginado del repositorio
            var (products, totalCount) = await _productRepository
                .GetPagedAsync(
                    request.PageNumber,
                    request.PageSize,
                    request.SearchTerm,
                    request.SortBy,
                    request.SortDesc
                );

            var productDtos = _mapper.Map<List<ProductDto>>(products);

            // Devuelve el resultado paginado
            return new PaginatedResult<ProductDto>(
                productDtos,
                totalCount,
                request.PageNumber,
                request.PageSize
            );
        }
    }

}
