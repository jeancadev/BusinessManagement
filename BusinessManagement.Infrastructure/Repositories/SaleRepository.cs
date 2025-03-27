using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Domain.Entities;
using BusinessManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessManagement.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly BusinessManagementDbContext _context;

        public SaleRepository(BusinessManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            // Incluimos la colección de Items para tener la venta completa
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .Include(s => s.Items)
                .ToListAsync();
        }

        public async Task AddAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Sale sale)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retorna una lista de ventas paginadas junto con el total de registros 
        /// </summary>
        /// <param name="pageNumber">Número de página (1-based)</param>
        /// <param name="pageSize">Tamaño de página (número de registros por página)</param>
        /// <param name="searchTerm">Texto opcional para filtrar</param>
        /// <returns>Una tupla con la lista de ventas y el conteo total.</returns>
        public async Task<(List<Sale>, int)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm)
        {
            // Construye la query base
            var query = _context.Sales
                .Include(s => s.Items)
                .AsQueryable();

            // Ejemplo: Si deseo filtrar por algo de la venta o del cliente,
            // podría unir con la tabla Customer o aplicar otros filtros.
            // if (!string.IsNullOrEmpty(searchTerm))
            // {
            //     query = query.Where(s => s.AlgunCampo.Contains(searchTerm));
            // }

            var totalCount = await query.CountAsync();

            // Ordena por fecha descendente, luego aplicamos skip/take
            var sales = await query
                .OrderByDescending(s => s.SaleDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (sales, totalCount);
        }
    }
}
