using LangrisserHelper.Server.Data;
using LangrisserHelper.Server.Services;
using LangrisserHelper.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LangrisserHelper.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }


        [HttpGet("GetAuthorityLevel")]
        public async Task<IActionResult> GetAuthorityLevel() 
        {
            var user = await _utilityService.GetUser();
            return Ok(user.Authority);
        }
    }
}
