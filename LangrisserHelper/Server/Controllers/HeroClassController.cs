
using LangrisserHelper.Server.Data;
using LangrisserHelper.Shared;
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
    public class HeroClassController : ControllerBase
    {
        private readonly DataContext _context;

        public HeroClassController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> BuildHeroClass(HeroClassResponse heroClassResponse)
        {
            var hero = await _context.Heros.FirstOrDefaultAsync<Hero>(u => u.HeroId == heroClassResponse.HeroId);
            var army = await _context.ArmyBranches.FirstOrDefaultAsync<ArmyBranch>(u => u.ArmyBranchId == heroClassResponse.ArmyBranchId);
            if (hero == null) 
            {
                return BadRequest("존재하지 않는 캐릭터");
            }
            Hero_Class newHeroClass = new Hero_Class()
            {
                HeroId = hero.HeroId,
                ArmyBranchId = army.ArmyBranchId,
                IsSP = heroClassResponse.IsSP,
                HeroClassName = heroClassResponse.HeroClassName
            };
            await _context.HeroClasses.AddAsync(newHeroClass);
            await _context.SaveChangesAsync();
            return Ok(newHeroClass);
        }

        [HttpGet("{HeroId}")]
        public async Task<IActionResult> GetHeroClass(int HeroId) 
        {
            var hero = await _context.Heros.FirstOrDefaultAsync<Hero>(u => u.HeroId == HeroId);
            if (hero == null)
            {
                return BadRequest("존재하지 않는 캐릭터");
            }

            var heroClasses = await _context.HeroClasses.Where(heroClass => heroClass.HeroId == hero.HeroId).ToListAsync();
            //var response = heroClasses.Select(
            //      unit => new HeroClassResponse 
            //        {
            //            HeroId = unit.HeroId,
            //            ArmyBranchId = unit.ArmyBranchId,
            //            IsSP = unit.IsSP,
            //            HeroClassName = unit.HeroClassName
            //        }
            //    );
            return Ok(heroClasses);
        }
    }
}
