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
    public class HeroController : ControllerBase
    {
        private readonly DataContext _context;

        public HeroController(DataContext context) 
        {
            _context = context;
        }
        public IList<Hero> HeroList { get; set; } = new List<Hero> { 
            new Hero { HeroId = 1, HeroName = "알테뮬러" }, new Hero { HeroId = 2, HeroName = "레온" }, new Hero { HeroId = 3, HeroName = "에마링크" } 
        };

        [HttpGet]
        public async Task<IActionResult> GetHero() 
        {
            var heros = await _context.Heros.ToListAsync();
            return Ok(heros);
        }
        [HttpPost]
        public async Task<IActionResult> AddHero(Hero hero) // json, postman으로 add 가능, id불필요
        {
            await _context.Heros.AddAsync(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.Heros.ToListAsync());
        }
        [HttpPut("{HeroId}")]
        public async Task<IActionResult> UpdateUnit(int id, Hero hero)
        {
            Hero dbHero = await _context.Heros.FirstOrDefaultAsync(u => u.HeroId == id);
            if (dbHero == null)
            {
                return NotFound("Hero with the given id doesn't exist.");
            }
            dbHero.HeroName = hero.HeroName;
            await _context.SaveChangesAsync();
            return Ok(dbHero);

        }
        [HttpDelete("{HeroId}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            Hero dbHero = await _context.Heros.FirstOrDefaultAsync(u => u.HeroId == id);
            if (dbHero == null)
            {
                return NotFound("Hero with the given id doesn't exist.");
            }
            _context.Heros.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.Heros.ToListAsync());

        }
    }
}
