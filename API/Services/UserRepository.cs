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

        public async Task<AppUser> GetUserAsync(int userId)
        {
           return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}