using Blazored.Toast.Services;
using LangrisserHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LangrisserHelper.Client.Services
{
    public class HeroService : IHeroService
    {
        private readonly HttpClient _http;
        public HeroService(HttpClient http, IToastService toastService)
        {
            this._http = http;
            _toastService = toastService;
        }

        //public List<Hero> HeroList { get; set; } = new List<Hero> { new Hero { HeroId=1, HeroName="알테뮬러" }, new Hero { HeroId = 2, HeroName = "레온" }, new Hero { HeroId = 3, HeroName = "에마링크" } };
        public List<Hero> HeroList { get; set; } = new List<Hero>();
        public IToastService _toastService { get; }

        public Dictionary<int, List<Hero_Class>> heroClassDic = new Dictionary<int, List<Hero_Class>>();



        public async Task LoadHeroClassAsync(int HeroId)
        {
            if (heroClassDic.ContainsKey(HeroId))
                return;


            var result = await _http.GetFromJsonAsync<List<Hero_Class>>($"api/HeroClass/{HeroId}");
            if (result != null)
                heroClassDic.Add(HeroId, result);
        }

        public async Task LoadHerosAsync()
        {
            if (HeroList.Count == 0)
            {
                HeroList = await _http.GetFromJsonAsync<List<Hero>>("api/hero");
            }
        }

        public async Task AddHeroClass(int heroId, int armyBranchId, string heroClassName, bool IsSP) 
        {
            HeroClassResponse heroClassResponse = new HeroClassResponse();
            heroClassResponse.HeroId = heroId;
            heroClassResponse.ArmyBranchId = armyBranchId;
            heroClassResponse.IsSP = IsSP;
            heroClassResponse.HeroClassName = heroClassName;
            var result = await _http.PostAsJsonAsync<HeroClassResponse>("api/HeroClass", heroClassResponse);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else 
            {
                // 저장된것 새로 불러오기
            }
        }

        public async Task<List<Hero_Class>> GetHeroClasses(int heroId)
        {
            await LoadHeroClassAsync(heroId);

            if (!heroClassDic.ContainsKey(heroId))
                return new List<Hero_Class>();

            return heroClassDic[heroId];
        }
    }
}
