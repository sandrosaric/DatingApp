using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DbContexts;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DatingAppContext _context;

        public UserRepository(DatingAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddUserAsync(AppUser user)
        {
            if(user == null){
                throw new ArgumentNullException(nameof(user));
            }

            await _context.AddAsync(user);
        }

        public async Task<AppUser> GetUserAsync(int userId)
        {
           return await _context.Users.FindAsync(userId);
        }

        public async Task<AppUser> GetUserAsync(string username)
        {
           if(username == null)
            throw new ArgumentNullException(nameof(username));

            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}