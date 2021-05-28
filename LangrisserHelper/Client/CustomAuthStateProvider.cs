using Blazored.LocalStorage;
using LangrisserHelper.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.IdentityModel;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace LangrisserHelper.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
        {
            this._localStorageService = localStorageService;
            _http = http;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string authToken = await _localStorageService.GetItemAsStringAsync("authToken");

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(authToken)) 
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                    // 로그인시 받아오고싶은 것이 있으면 여기서 service inject 후 Get메소드를 calㅣ함
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    await _localStorageService.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }
            AuthenticationState state = null;
            await Task.Run(() => {
                var user = new ClaimsPrincipal(identity);
                
                state = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(state));
            });
            return state;

            //var state = new AuthenticationState(new ClaimsPrincipal());
            //if (await _localStorageService.GetItemAsync<bool>("isAuthenticated")) 
            //{
            //    var identity = new ClaimsIdentity(
            //        new[]
            //        {
            //        new Claim(ClaimTypes.Name, "Sakura")
            //        }, "test authenication type");
            //    var user = new ClaimsPrincipal(identity);
            //    state = new AuthenticationState(user);

            // }
            //NotifyAuthenticationStateChanged(Task.FromResult(state));
            //return state;
        }
        //public async Task CustomNotifyAuthenticationStateChanged() 
        //{
        //    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        //}

        private byte[] TmpParseBase64WithoutPadding(string base64)
        {
            System.Diagnostics.Debug.WriteLine(base64.Length);
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public IEnumerable<Claim> TmpParseClaimsFromJwt(string jwt)
        {
            System.Diagnostics.Debug.WriteLine(jwt);
            string payload = jwt.Split('.')[1];
            byte[] jsonBytes = ParseBase64WithoutPadding(payload);
            Dictionary<string, object> keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            IEnumerable<Claim> claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            return claims;
        }

        public IEnumerable<Claim> ParseClaimsFromJwt(string jwt) 
        {
            System.Diagnostics.Debug.WriteLine(jwt);
            jwt = jwt.Replace("\"","");
            var decoded2 = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
            return decoded2.Claims;
        }
    }
}
