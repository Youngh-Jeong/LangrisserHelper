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
    public class BaseService : IBaseService
    {
        private readonly HttpClient _http;
        private readonly IToastService _toastService;

        public BaseService(HttpClient http, IToastService toastService)
        {
            _http = http;
            _toastService = toastService;
        }
        public List<ArmyBranch> ArmyBranchList { get; set; } = new List<ArmyBranch>();

        public async Task LoadArmyBranchesAsync()
        {
            if (ArmyBranchList.Count == 0)
            {
                ArmyBranchList = await _http.GetFromJsonAsync<List<ArmyBranch>>("api/armybranch");
            }
        }
    }
}
