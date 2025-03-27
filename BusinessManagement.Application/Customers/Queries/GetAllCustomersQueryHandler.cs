using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Application.Customers.DTOs;
using MediatR;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Customers.Queries
{
    public class GetAllCustomersQueryHandler
        : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> Handle(
            GetAllCustomersQuery request,
            CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync();

            // Mapeo de Customer -> CustomerDto
            return _mapper.Map<List<CustomerDto>>(customers);
        }
    }
}
