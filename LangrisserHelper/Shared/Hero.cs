using System;
using System.Collections.Generic;
using System.Text;

namespace LangrisserHelper.Shared
{
    public class Hero
    {
        public int HeroId { get; set; }
        public string HeroName { get; set; }
        public List<Hero_Class> HeroClassses { get; set; }
    }
    public class Hero_Class
    {
        public int Id { get; set; }
        public int HeroId { get; set; }
        public ArmyBranch ArmyBranch { get; set; }
        public int ArmyBranchId { get; set; }
        public string HeroClassName { get; set; }
        public bool IsSP { get; set; }
    }
    public class My_Hero_Class
    {
        public int Id { get; set; } 
        public int HeroClassId{ get; set; }
        public int UserId { get; set; }
        public Hero_Class HeroClass { get; set; }
    }
}
