using LangrisserHelper.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LangrisserHelper.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }

        public DbSet<Hero> Heros { get; set; } // db의 이름이 됨
        public DbSet<User> Users { get; set; }
        public DbSet<Faction> Factions { get; set; } // 추가하는법: 클래스만들기, 여기에 추가하기, dotnet ef migrations add "", dotnet ef database update 
        public DbSet<ArmyBranch> ArmyBranches { get; set; }
        public DbSet<Hero_Class> HeroClasses { get; set; }
        public DbSet<My_Hero_Class> UserRoasters { get; set; }
    }
}
