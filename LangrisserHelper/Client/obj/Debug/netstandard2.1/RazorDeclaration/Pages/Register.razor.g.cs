// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace LangrisserHelper.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using LangrisserHelper.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using LangrisserHelper.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Blazored.Toast;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Blazored.Toast.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/register")]
    public partial class Register : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 62 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\Pages\Register.razor"
       
    LangrisserHelper.Shared.UserRegister user = new LangrisserHelper.Shared.UserRegister();

    async void HandleRegistration()
    {
        var result = await AuthService.Register(user);
        if (result.Success)
        {
            ToastService.ShowSuccess(result.Message);
            NavigationManager.NavigateTo("");
        }
        else 
        {
            ToastService.ShowError(result.Message);
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IToastService ToastService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private LangrisserHelper.Client.Services.IAuthService AuthService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private LangrisserHelper.Client.Services.IHeroService HeroService { get; set; }
    }
}
#pragma warning restore 1591
