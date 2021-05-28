using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LangrisserHelper.Server.Engine;
using LangrisserHelper.Server.Hubs;
using LangrisserHelper.Shared;
using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalRApp.Server.Hubs
{
    public class TestGameHub : Hub
    {
        static ConcurrentDictionary<string, TestGame> clientList = new ConcurrentDictionary<string, TestGame>(); // groupName - othersKey
        static ConcurrentDictionary<string, string> userList = new ConcurrentDictionary<string, string>(); // contextId - groupName
        // 1) 아무도 없으면, key값이 없음.(방에서 모두 사라지면 list에서 삭제해야 함) 2) 방을 만들면, 방이름-생성자 가 추가됨 3) 
        public async Task<int> PeopleInRoom(string groupName) 
        {
            if (!clientList.ContainsKey(groupName))
            {
                return 0;
            }
            else if (clientList[groupName] == null || clientList[groupName].Players == null)
            {
                return 0;
            }
            else 
            {
                return clientList[groupName].Players.Count;
            }
        }
        public async Task AddToGroup(string groupName, Player player)
        {
            player.Id = Context.ConnectionId;
            TestGame game = clientList.GetOrAdd(groupName, new TestGame(groupName, player));
            if (game.CanJoin)
            {
                userList.AddOrUpdate(Context.ConnectionId, groupName, (key, value) => groupName);
                string tmp = userList.GetOrAdd(Context.ConnectionId, groupName);
                game.Join(player);
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                await Clients.Group(groupName).SendAsync("Send", $"{player.Name}가 {groupName}에 입장하였습니다.");
                await Clients.OthersInGroup(groupName).SendAsync("OpponentChanged", player);
                if (game.Players.Count == 2) 
                {
                    await Clients.Caller.SendAsync("OpponentChanged", game.Opponent(player.Id));
                }

            }
            else
            {
                await Clients.Caller.SendAsync("Send", "Full Room");
            }
        }
        public async Task RemoveFromGroup(string groupName, Player player)
        {
            if (player == null)
                player = new Player();
            player.Id = Context.ConnectionId;
            if (!clientList.ContainsKey(groupName))
                return;
            TestGame game = clientList[groupName];
            game.Leave(player);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).SendAsync("OpponentChanged", null);

            if (!String.IsNullOrEmpty(player.Name)) 
            {
                await Clients.OthersInGroup(groupName).SendAsync("Send", $"{player.Name}가 {groupName}를 떠났습니다.");
            }
        }
        public async Task Ready(string groupName, bool IsReady) 
        {
            if (!clientList.ContainsKey(groupName))
                return;
            clientList[groupName].Me(Context.ConnectionId).IsReady = IsReady;
            await Clients.OthersInGroup(groupName).SendAsync("OpponentReadyChanged", IsReady); // 상대방에게 내 레디상태를 보냄


            if (clientList[groupName].Players.Count == 2 && clientList[groupName].Players.All(p => p.IsReady))
                await GameStart(groupName);
                    //await Clients.Group(groupName).SendAsync("GameStart"); // 모두 레디했으면 GameStart를 실행함
        }

        public async Task Picked(string groupName, List<int> pickedList)
        {
            if (!clientList.ContainsKey(groupName))
                return;
            clientList[groupName].Me(Context.ConnectionId).Pick(pickedList);
            await Clients.OthersInGroup(groupName).SendAsync("OpponentChanged", clientList[groupName].Me(Context.ConnectionId));
            await Clients.Group(groupName).SendAsync("TurnChanged", 0);
        }
        public async Task Banned(string groupName, List<int> bannedList)
        {
            if (!clientList.ContainsKey(groupName))
                return;
            clientList[groupName].Opponent(Context.ConnectionId).Ban(bannedList);
            await Clients.OthersInGroup(groupName).SendAsync("MeChanged", clientList[groupName].Opponent(Context.ConnectionId));
            await Clients.Group(groupName).SendAsync("TurnChanged", 0);
        }
        public async Task Message(string groupName, string msg) 
        {
            await Clients.Group(groupName).SendAsync("Send", $"{clientList[groupName].Me(Context.ConnectionId).Name} : {msg}");
        }


        private async Task GameStart(string gameName) 
        {
            TestGame game = clientList[gameName];
            int map = await game.SetRandomMap();
            string firstUserName = await game.SetFirst(Context.ConnectionId);

            await Clients.Group(gameName).SendAsync("GameStart", map, firstUserName);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string groupName = userList[Context.ConnectionId];
            Player player = clientList[groupName].Me(Context.ConnectionId);
            await RemoveFromGroup(groupName, player);
            //  var removeTask = clientList.Select(c => c.Value.LeaveAsync(Context.ConnectionId));
            // await Task.WhenAll(removeTask);
            await base.OnDisconnectedAsync(exception);
        }
    }
}