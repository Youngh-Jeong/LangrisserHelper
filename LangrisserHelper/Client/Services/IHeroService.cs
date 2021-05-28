using LangrisserHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LangrisserHelper.Client.Services
{
    public interface IHeroService
    {
        List<Hero> HeroList { get; set; }
        Task<List<Hero_Class>> GetHeroClasses(int heroId);
        Task LoadHeroClassAsync(int heroClassId);
        Task LoadHerosAsync();
        Task AddHeroClass(int heroId, int armyBranchId, string heroClassName, bool IsSP);
    }
}
