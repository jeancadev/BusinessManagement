using BusinessManagement.Domain.Entities;

namespace BusinessManagement.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByUsernameAsync(string username);
    }
}
