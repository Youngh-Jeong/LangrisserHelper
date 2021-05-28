using LangrisserHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LangrisserHelper.Server.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string name, string password);
        Task<bool> UserExists(string name);
        Task<int> GetIdFromToken(string token);
    }
}
