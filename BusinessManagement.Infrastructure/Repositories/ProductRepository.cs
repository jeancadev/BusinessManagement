using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Domain.Entities;
using BusinessManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BusinessManagementDbContext _context;

        public ProductRepository(BusinessManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<(List<Product>, int)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm,
            string? sortBy,
            bool sortDesc)
        {
            var query = _context.Products.AsQueryable();

            // Filtro
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm)
                                      || p.Description.Contains(searchTerm));
            }

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortBy switch
                {
                    "Price" => sortDesc ? query.OrderByDescending(p => p.Price)
                                        : query.OrderBy(p => p.Price),
                    "Name" => sortDesc ? query.OrderByDescending(p => p.Name)
                                        : query.OrderBy(p => p.Name),
                    _ => query.OrderBy(p => p.Id)
                };
            }
            else
            {
                query = query.OrderBy(p => p.Id);
            }

            var totalCount = await query.CountAsync();
            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            // EF Core rastrea cambios si la entidad está adjunta   
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
