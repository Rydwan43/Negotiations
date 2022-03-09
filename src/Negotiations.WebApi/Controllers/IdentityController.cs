using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Negotiations.Application.Models;
using Negotiations.Application.Interfaces;

namespace Negotiations.WebApi.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService _userService;
        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginUserDto loginUser)
        {
            var token = _userService.GenerateJwt(loginUser);

            if (token is null)
            {
                return BadRequest("Bad username or password");
            }

            return Ok(token);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterUserDto registerUser)
        {
            _userService.RegisterUser(registerUser);
            return NoContent();
        }
    }
}