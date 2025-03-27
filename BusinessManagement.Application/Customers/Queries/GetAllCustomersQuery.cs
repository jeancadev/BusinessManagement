using BusinessManagement.Application.Customers.DTOs;
using MediatR;
using System.Collections.Generic;

namespace BusinessManagement.Application.Customers.Queries
{
    public record GetAllCustomersQuery() : IRequest<List<CustomerDto>>;
}
