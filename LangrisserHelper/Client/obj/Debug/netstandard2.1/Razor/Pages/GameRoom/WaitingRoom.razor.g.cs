#pragma checksum "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d41e2fcfd56aea1dca4b05fd8f966902a53d2d02"
// <auto-generated/>
#pragma warning disable 1591
namespace LangrisserHelper.Client.Pages.GameRoom
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using LangrisserHelper.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Blazored.Toast;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Blazored.Toast.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
using LangrisserHelper.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
using LangrisserHelper.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
using Microsoft.AspNetCore.SignalR.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
           [Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Room")]
    public partial class WaitingRoom : Microsoft.AspNetCore.Components.ComponentBase, IDisposable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h2>Room</h2>\r\n");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "style", "width:500px; float:left;");
#nullable restore
#line 15 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
     if (RoasterService.MyRoaster.Count == 15)
    {

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<LangrisserHelper.Client.Pages.Component.RoasterViewer>(3);
            __builder.AddAttribute(4, "RoasterId", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.List<System.Int32>>(
#nullable restore
#line 17 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
                                                                            RoasterService.MyRoasterIds

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(5, "Banned", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.List<System.Int32>>(
#nullable restore
#line 17 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
                                                                                                                  new List<int>()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(6, "Picked", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.List<System.Int32>>(
#nullable restore
#line 17 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
                                                                                                                                           new List<int>()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(7, "IsOpponent", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 17 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
                                                                                                                                                                        false

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
#nullable restore
#line 18 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
    }
    else
    {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(8, "<div>로스터가 세팅되어있지 않습니다.</div>");
#nullable restore
#line 22 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\r\n\r\n");
            __builder.OpenElement(10, "div");
            __builder.AddAttribute(11, "style", "width:500px; float:left;");
            __builder.AddMarkupContent(12, "<h6>방 이름</h6>\r\n    ");
            __builder.OpenElement(13, "input");
            __builder.AddAttribute(14, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 27 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
                  groupName

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(15, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => groupName = __value, groupName));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n    ");
            __builder.OpenElement(17, "button");
            __builder.AddAttribute(18, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 28 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
                      ButtonClick

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(19, "입장");
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 31 "D:\새 폴더 (2)\LangrisserHelper\LangrisserHelper\Client\Pages\GameRoom\WaitingRoom.razor"
       
    private string alertMessage;
    private string token;
    public string groupName;
    public string myName;
    public bool IsConnected { get => gameClient != null && gameClient.Connected; }
    public HubConnectionState State { get; set; } = HubConnectionState.Disconnected;
    public string StateMessage { get; set; }
    Player player;
    List<Player> players = new List<Player>();
    GameClient gameClient;
    Player Opponent;

    List<String> Messages = new List<string>();

    private async Task ButtonClick()
    {
        await EnterRoom();
    }

    async Task EnterRoom()
    {
        if (RoasterService.MyRoaster.Count == 15)
            NavManager.NavigateTo($"/Room/{groupName}");
    }
    void MessageArrived(string text)
    {
        Messages.Add(text);
        StateHasChanged();
        System.Diagnostics.Debug.WriteLine(text);
    }
    void OpponentChanged(Player opponent)
    {
        StateHasChanged();
    }

    public void Dispose()
    {
        if (gameClient != null && gameClient.Connected)
        {
            gameClient.MessageArrived -= MessageArrived;
        }
        Stop();
        gameClient = null;
    }

    async Task Stop()
    {
        if (gameClient != null && gameClient.Connected)
        {
            await gameClient.LeaveAsync();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            token = await LocalStorage.GetItemAsync<string>("authToken");
            await HeroService.LoadHerosAsync();
            await RoasterService.LoadRoasterAsync(token);
            RoasterService.OnChange += StateHasChanged;
            await InvokeAsync(StateHasChanged);
        }
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JsRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private LangrisserHelper.Client.Services.IRoasterService RoasterService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private LangrisserHelper.Client.Services.IHeroService HeroService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; }
    }
}
#pragma warning restore 1591