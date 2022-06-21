using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserAsync(int userId);

        Task<AppUser> GetUserAsync(string username);

        Task AddUserAsync(AppUser user);
        Task<bool> SaveChangesAsync();

        Task<bool> UserExistsAsync(string username);
    }
}