using BusinessManagement.Application.Customers.DTOs;
using MediatR;
using System;

namespace BusinessManagement.Application.Customers.Queries
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto?>;
}
