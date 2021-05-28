using LangrisserHelper.Server.Data;
using LangrisserHelper.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LangrisserHelper.Server.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthRepository _authRepository;

        public UtilityService(DataContext context, IHttpContextAccessor httpContextAccessor, IAuthRepository authRepository)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _authRepository = authRepository;
        }
        public async Task<int> GetUserId(string token) 
        {
            return await _authRepository.GetIdFromToken(token);
        }
        public async Task<User> GetUser()
        {

            System.Diagnostics.Debug.WriteLine(ClaimTypes.NameIdentifier);
            System.Diagnostics.Debug.WriteLine("1---------------------");
            System.Diagnostics.Debug.WriteLine(_httpContextAccessor == null);
            System.Diagnostics.Debug.WriteLine("---------------------");
            System.Diagnostics.Debug.WriteLine("2---------------------");
            System.Diagnostics.Debug.WriteLine(_httpContextAccessor.HttpContext == null);
            System.Diagnostics.Debug.WriteLine("---------------------");
            System.Diagnostics.Debug.WriteLine("3---------------------");
            System.Diagnostics.Debug.WriteLine(_httpContextAccessor.HttpContext.User == null);
            System.Diagnostics.Debug.WriteLine("---------------------");
            var test = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var test2 = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            System.Diagnostics.Debug.WriteLine("4---------------------");
            System.Diagnostics.Debug.WriteLine(test);
            System.Diagnostics.Debug.WriteLine(test2);
            System.Diagnostics.Debug.WriteLine("---------------------");
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }
    }
}
