using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DbContexts;
using API.Entities;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DatingAppContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DatingAppContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddUserAsync(AppUser user)
        {
            if(user == null){
                throw new ArgumentNullException(nameof(user));
            }

            await _context.AddAsync(user);
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users.Where(x => x.UserName == username).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
           return await _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int userId)
        {
           return await _context.Users.FindAsync(userId);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
           if(username == null)
            throw new ArgumentNullException(nameof(username));

            return await _context.Users.Include(x => x.Photos).SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.Include(x => x.Photos).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync() > 0);
        }

        public void  UpdateAsnyc(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}