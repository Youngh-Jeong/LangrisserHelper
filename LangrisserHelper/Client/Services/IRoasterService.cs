using LangrisserHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LangrisserHelper.Client.Services
{
    public interface IRoasterService
    {
        event Action OnChange;
        List<My_Hero_Class> MyRoaster { get; set; }
        List<int> MyRoasterIds { get; }
        void AddRoaster(Hero_Class hero);
        void RemoveRoaster(int heroClassId);
        Task SaveRoaster(string token);
        Task LoadRoasterAsync(string token);
        List<Hero_Class> UserRoaster { get; set; }

        List<Hero_Class> OpponentRoaster { get; set; }
        Task LoadRoasterByIdAsync(List<int> idArray, bool IsOpponent);
        Hero_Class HeroClassByIdInRoaster(int id, bool IsOpponent);
    }
}
