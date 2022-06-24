using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Models;

namespace API.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int userId);

        Task<AppUser> GetUserByUsernameAsync(string username);

        Task AddUserAsync(AppUser user);

        void UpdateAsnyc(AppUser user);
        Task<bool> SaveChangesAsync();

        Task<bool> UserExistsAsync(string username);

        Task<IEnumerable<MemberDto>> GetMembersAsync();

        Task<MemberDto> GetMemberAsync(string username);


    }
}