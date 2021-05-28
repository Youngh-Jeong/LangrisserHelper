using LangrisserHelper.Server.Data;
using LangrisserHelper.Server.Services;
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
    public class UserRoasterController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserRoasterController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserRoaster()
        {
            string tkn = Request.Headers["Authorization"];
            var userId = await _utilityService.GetUserId(tkn.Substring(7,tkn.Length-7));
            var roaster = await _context.UserRoasters.Where(r => r.UserId == userId).ToListAsync();
            roaster.ForEach(f => f.HeroClass = _context.HeroClasses.FirstOrDefault<Hero_Class>(h => h.Id == f.HeroClassId));
            return Ok(roaster);
        }


        [HttpGet("ById/{idArray}")]
        public async Task<IActionResult> GetRoasterById([FromRoute] string idArray)
        {
            List<Hero_Class> heroClassList = new List<Hero_Class>();
            var idList = idArray.Split("@").ToList();

            foreach (var id in idList)
            {
                System.Diagnostics.Debug.WriteLine(id);
                heroClassList.Add(await GetHeroClassById(id));
            }
            await Task.Delay(10);
            return Ok(heroClassList);
        }

        private async Task<Hero_Class> GetHeroClassById(string id)
        {
            return await _context.HeroClasses.Where(heroClass => heroClass.Id == Convert.ToInt32(id)).FirstOrDefaultAsync();
        }

        [HttpPost("{heroClassId}")]
        public async Task<IActionResult> BuildUserRoaster([FromRoute] string heroClassId, [FromBody] string token) 
        {

            System.Diagnostics.Debug.WriteLine(heroClassId);
            var heroClassIds = heroClassId.Split("@"); 

            var userId = await _utilityService.GetUserId(token);

            var oldRoaster = await _context.UserRoasters.Where(r => r.UserId == userId).ToArrayAsync();
            await Task.Run(() => { _context.UserRoasters.RemoveRange(oldRoaster); });

            var tasks = heroClassIds.ToList().Select(i => SetUserRoaster(userId, i));

            await Task.WhenAll(tasks);
            await _context.SaveChangesAsync();

            return Ok(heroClassId);

        }

        private async Task SetUserRoaster(int userId, string idString) 
        {
            int idInt = Int32.Parse(idString);
            var unit = await _context.HeroClasses.FirstOrDefaultAsync<Hero_Class>(h => h.Id == idInt);
            My_Hero_Class newUserHeroClass = new My_Hero_Class
            {
                UserId = userId,
                HeroClassId = unit.Id,
                HeroClass = unit
            };
            await _context.UserRoasters.AddAsync(newUserHeroClass);
        }
    }
}
