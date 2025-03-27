using BusinessManagement.Application.Inventories.DTOs;
using MediatR;
using System.Collections.Generic;

namespace BusinessManagement.Application.Inventories.Queries
{
    public record GetAllInventoriesQuery() : IRequest<List<InventoryDto>>;
}
