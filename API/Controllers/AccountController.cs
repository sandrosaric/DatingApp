using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AccountController(IUserRepository userRepository,ITokenService tokenService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException((nameof(userRepository)));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if(await _userRepository.UserExistsAsync(registerDto.Username))
                return BadRequest("Username is already taken");
            using var hmac = new HMACSHA512();

            var user = new AppUser(){
                UserName = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();


              return new UserDto(){
                Username= user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }  



        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){

            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);

            if(user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0; i < computedHash.Length; i++){
                if(computedHash[i] != user.PasswordHash[i]){
                    return Unauthorized("Invalid password");
                } 
            }

            return new UserDto(){
                Username= user.UserName,
                Token = _tokenService.CreateToken(user)
            };


        }


    
    }
}