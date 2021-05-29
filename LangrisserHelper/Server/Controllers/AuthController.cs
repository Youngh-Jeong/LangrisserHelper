using LangrisserHelper.Server.Data;
using LangrisserHelper.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LangrisserHelper.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo) 
        {
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        [Consumes("application/json")]
        public async Task<IActionResult> Register(UserRegister request) 
        {
            var response = await _authRepo.Register(
                new User {
                    Username = request.UserName,
                    Email = request.Email,
                    Authority = 0,
                    IsConfirmed = request.IsConfirmed
                }, request.Password
                );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        public async Task<IActionResult> Login(UserLogin request)
        {
            Console.WriteLine((request??new UserLogin()).UserName);
            var response = await _authRepo.Login(request.UserName, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
