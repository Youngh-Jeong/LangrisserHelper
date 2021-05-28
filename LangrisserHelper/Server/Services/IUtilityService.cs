using LangrisserHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LangrisserHelper.Server.Services
{
    public interface IUtilityService
    {
        Task<User> GetUser();
        Task<int> GetUserId(string token);
    }
}
