using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Application.Common.Models;
using BusinessManagement.Application.Sales.DTOs;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Sales.Queries
{
    public class GetAllSalesPagedQueryHandler
        : IRequestHandler<GetAllSalesPagedQuery, PaginatedResult<SaleDto>>
    {
        private readonly ISaleRepository _saleRepository;

        public GetAllSalesPagedQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<PaginatedResult<SaleDto>> Handle(
            GetAllSalesPagedQuery request,
            CancellationToken cancellationToken)
        {
            var (sales, totalCount) = await _saleRepository
                .GetPagedAsync(request.PageNumber, request.PageSize, request.SearchTerm);

            var saleDtos = sales.Select(s => new SaleDto
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                SaleDate = s.SaleDate,
                TotalAmount = s.TotalAmount,
                Items = s.Items.Select(i => new SaleItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            }).ToList();

            return new PaginatedResult<SaleDto>(
                saleDtos,
                totalCount,
                request.PageNumber,
                request.PageSize
            );
        }
    }
}
