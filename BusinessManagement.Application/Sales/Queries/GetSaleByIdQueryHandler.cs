using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Application.Sales.DTOs;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Sales.Queries
{
    public class GetSaleByIdQueryHandler
        : IRequestHandler<GetSaleByIdQuery, SaleDto?>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleByIdQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleDto?> Handle(
            GetSaleByIdQuery request,
            CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);
            if (sale == null)
                return null;

            return _mapper.Map<SaleDto>(sale);
        }
    }
}
