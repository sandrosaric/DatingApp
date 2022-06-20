using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException((nameof(userRepository)));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAll(){
         
            return Ok(await  _userRepository.GetUsersAsync());
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult<IEnumerable<AppUser>>> Get(int userId){
            var user = await _userRepository.GetUserAsync(userId);

            if(user == null){
                return NotFound();
            }
            return Ok(user);
        } 
    }
}