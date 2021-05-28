using LangrisserHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LangrisserHelper.Client.Services
{
    interface IBaseService
    {
        List<ArmyBranch> ArmyBranchList { get; set; }
        Task LoadArmyBranchesAsync();

    }
}
