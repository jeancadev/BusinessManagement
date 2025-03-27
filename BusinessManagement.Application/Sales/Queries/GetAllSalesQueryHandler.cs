using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Application.Sales.DTOs;
using MediatR;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Sales.Queries
{
    public class GetAllSalesQueryHandler
        : IRequestHandler<GetAllSalesQuery, List<SaleDto>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetAllSalesQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<List<SaleDto>> Handle(
            GetAllSalesQuery request,
            CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllAsync();
            return _mapper.Map<List<SaleDto>>(sales);
        }
    }
}
