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
#nullable restore
#line 2 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\Pages\RoomTest.razor"
using LangrisserHelper.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\Pages\RoomTest.razor"
using LangrisserHelper.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\Pages\RoomTest.razor"
using Microsoft.AspNetCore.SignalR.Client;

#line default
#line hidden
#nullable disable
    public partial class RoomTest : Microsoft.AspNetCore.Components.ComponentBase, IDisposable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 29 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\Pages\RoomTest.razor"
       
    // 로스터 진행
    private bool IsVisibleMyRoaster;


    //
    [Parameter]
    public string groupName { get; set; }
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
        if (string.IsNullOrEmpty(groupName))
            groupName = "테스트방";
        if (player == null)
        {
            player = new Player();
            player.JoinedOn = DateTime.Now;
            player.Name = myName;
        }
        if (gameClient == null || !gameClient.Connected)
        {
            gameClient = new GameClient(groupName, player, NavManager.BaseUri);
            gameClient.MessageArrived += MessageArrived;
            gameClient.OpponentChanged += OpponentChanged;
            await gameClient.EnterAsync();
            State = HubConnectionState.Connected;
            StateMessage = "Game started.";

            //await JsRuntime.FocusAsync(_canvasContainer);
            StateHasChanged();
        }

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

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JsRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavManager { get; set; }
    }
}
#pragma warning restore 1591
