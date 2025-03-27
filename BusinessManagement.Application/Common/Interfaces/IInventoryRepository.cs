using BusinessManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Common.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory?> GetByIdAsync(Guid id);
        Task<List<Inventory>> GetAllAsync();
        Task AddAsync(Inventory inventory);
        Task UpdateAsync(Inventory inventory);
        Task DeleteAsync(Inventory inventory);
        Task<Inventory?> GetByProductIdAsync(Guid productId);
    }
}
