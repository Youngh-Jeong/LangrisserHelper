using System;
using System.Collections.Generic;
using System.Text;

namespace LangrisserHelper.Shared
{
    public class HeroClassResponse
    {
        public int HeroId { get; set; }
        public int ArmyBranchId { get; set; }
        public string HeroClassName { get; set; }
        public bool IsSP { get; set; }
    }
}
