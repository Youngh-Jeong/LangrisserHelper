using LangrisserHelper.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LangrisserHelper.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmyBranchController : ControllerBase
    {
        private readonly DataContext _context;

        public ArmyBranchController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetArmyBranch()
        {
            var armyBranches = await _context.ArmyBranches.ToListAsync();
            return Ok(armyBranches);
        }

    }
}
