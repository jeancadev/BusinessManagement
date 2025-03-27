using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Domain.Entities;
using BusinessManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly BusinessManagementDbContext _context;

    public UserRepository(BusinessManagementDbContext context)
    {
        _context = context;
    }

    public async Task<AppUser?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }
}
