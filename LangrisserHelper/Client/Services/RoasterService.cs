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
    public class RoasterService : IRoasterService
    {
        private readonly IToastService _toastService;
        private readonly HttpClient _http;
        public RoasterService(IToastService toastService, HttpClient http) 
        {
            this._http = http;
            this._toastService = toastService;
        }
        public event Action OnChange = delegate() { return; };
        public List<My_Hero_Class> MyRoaster { get; set; } = new List<My_Hero_Class>() {};

        public List<int> MyRoasterIds { get {
                return MyRoaster.Select(r => r.HeroClassId).ToList();
            } }

        public void AddRoaster(Hero_Class hero)
        {
            if (MyRoaster.Count >= 15)
                return;
            foreach (var roaster in MyRoaster) 
            {
                if (roaster.HeroClass.HeroId == hero.HeroId)
                    return;
            }
            My_Hero_Class myHero = new My_Hero_Class();
            myHero.HeroClass = new Hero_Class();
            myHero.UserId = 1;
            myHero.HeroClassId = hero.Id;
            myHero.HeroClass = hero;
            MyRoaster.Add(myHero);
            RoasterChanged();
        }

        public void RemoveRoaster(int heroClassId)
        {
            for (int i = 0; i<MyRoaster.Count; i++) 
            {
                if (MyRoaster[i].HeroClassId == heroClassId)
                {
                    MyRoaster.RemoveAt(i);
                    break;
                }
            }
            RoasterChanged();
        }

        public async Task SaveRoaster(string token)
        {
            string heroClassString = "";
            await Task.Run(() => {
                foreach (var myHero in MyRoaster) 
                {
                    heroClassString += $"@{myHero.HeroClassId}";
                }
                heroClassString = heroClassString.Substring(1, heroClassString.Length-1);
            });
            System.Diagnostics.Debug.WriteLine(heroClassString);
            var result = await _http.PostAsJsonAsync<string>($"api/UserRoaster/{heroClassString}", token);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else
            {
                // 저장된것 새로 불러오기
                _toastService.ShowSuccess("로스터가 저장되었습니다.", ":)");
            }
        }
        void RoasterChanged() => OnChange.Invoke();

        public async Task LoadRoasterAsync(string token)
        {
                MyRoaster = await _http.GetFromJsonAsync<List<My_Hero_Class>>($"api/userroaster");
            RoasterChanged();
        }


        public List<Hero_Class> UserRoaster { get; set; } = new List<Hero_Class>();
        public List<Hero_Class> OpponentRoaster { get; set; } = new List<Hero_Class>();

        public async Task LoadRoasterByIdAsync(List<int> idArray, bool IsOpponent)
        {
            string idString = "";
            idArray.ForEach(async id => await idSum(id));
            await Task.Delay(2);
            idString = idString.Substring(0, idString.Length - 1);
            if (IsOpponent)
            {
                OpponentRoaster = await _http.GetFromJsonAsync<List<Hero_Class>>($"api/userroaster/ById/{idString}");
            }
            else if (UserRoaster.Count == 0) 
            {
                UserRoaster = await _http.GetFromJsonAsync<List<Hero_Class>>($"api/userroaster/ById/{idString}");
            }


            async Task idSum(int id)
            {
                idString += $"{id.ToString()}@";
                await Task.Delay(1);
            }
        }
        public Hero_Class HeroClassByIdInRoaster(int id, bool IsOpponent) 
        {
            if (IsOpponent)
            {
                return OpponentRoaster.Where(r => r.Id == id).FirstOrDefault();
            }
            else 
            {
                return UserRoaster.Where(r => r.Id == id).FirstOrDefault();
            }
        }
    }
}
