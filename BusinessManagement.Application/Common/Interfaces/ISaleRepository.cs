using BusinessManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessManagement.Application.Common.Interfaces
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(Guid id);
        Task<List<Sale>> GetAllAsync();
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task DeleteAsync(Sale sale);

        // Método para paginación
        Task<(List<Sale>, int)> GetPagedAsync(int pageNumber, int pageSize, string? searchTerm);
    }
}
