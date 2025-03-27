using BusinessManagement.Application.Inventories.DTOs;
using MediatR;
using System;

namespace BusinessManagement.Application.Inventories.Queries
{
    public record GetInventoryByIdQuery(Guid Id) : IRequest<InventoryDto?>;
}
