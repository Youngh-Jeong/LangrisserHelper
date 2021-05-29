using LangrisserHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LangrisserHelper.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            Console.WriteLine(_http.BaseAddress);
            var result = await _http.PostAsJsonAsync("api/Auth/login", request);
            Console.WriteLine(await result.Content.ReadAsStringAsync());
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("api/Auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
