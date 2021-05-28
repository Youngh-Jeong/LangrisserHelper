//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.SignalR;
//using LangrisserHelper.Shared;

//namespace LangrisserHelper.Server.Engine
//{
//    public class EngineService
//    {
//        private static readonly ConcurrentDictionary<string, Game> games = new ConcurrentDictionary<string, Game>();
//        public async Task HandleGameActionAsync(Hub hub, PlayerPlayedAction action)
//        {
//            action.ConnectionId = hub.Context.ConnectionId;
//            var game = games[action.GameId];

//            action.Player = game.Players.Find(p => p.Id.ToString() == hub.Context.ConnectionId);

//            bool moved = false;
//            switch (action.Payload.KeyCode)
//            {
//                case ActionKind.Ban:
//                    action.Player.Ban(action.Payload.Selected);
//                    break;
//                case ActionKind.Pick:
//                    action.Player.Pick(action.Payload.Selected);
//                    break;
//                default:
//                    break;
//            }

//            //hub.Context.Abort(); 연결 중단
//            // await HandlePlayerLeftAsync(hub, hub.Context.ConnectionId, lost: true); 플레이어가 나갔을 때 보내는 신호/값?
//            //await hub.Clients.Group(action.GameId).SendAsync(Constants.GAME_NEW_EGG, game.NewEgg()); 그룹에 속한 모든 사람들에게  보냄??
//            //await hub.Clients.Group(action.GameId).SendAsync(Constants.GAME_ACTION_CALLBACK, action); 마찬가지?
//        }

//        public async Task HandlePlayerJoinedAsync(Hub hub, PlayerJoinedAction action)
//        {

//            Player player = action.Payload.First();

//            player.Id = hub.Context.ConnectionId;


//            Game game = games.GetOrAdd(action.GameId, new Game(action.GameId, player));

//            game.Join(player);
//            action.Payload = game.Players;

//            await hub.Groups.AddToGroupAsync(hub.Context.ConnectionId, action.GameId);

//            action.Player = games[action.GameId]?.Players.Find(p => p.Id == hub.Context.ConnectionId);

//            await hub.Clients.Group(action.GameId).SendAsync(Constants.GAME_PLAYER_JOINED_CALLBACK, action);

//            await hub.Clients.Caller.SendAsync(Constants.GAME_NEW_EGG, game.Egg);
//        }
//        public async Task HandlePlayerLeftAsync(Hub hub, PlayerLeftAction action)
//        {
//            Player player = action.Payload.First();

//            player.Id = hub.Context.ConnectionId;

//            var game = games[action.GameId];
//            game.Leave(player);
//            action.Payload = game.Players;

//            await hub.Groups.RemoveFromGroupAsync(hub.Context.ConnectionId, action.GameId);

//            action.Player = player;

//            await hub.Clients.Group(action.GameId).SendAsync(Constants.GAME_PLAYER_LEFT_CALLBACK, action);
//        }

//        public async Task HandlePlayerLeftAsync(Hub hub, string connectionId, bool lost = false)
//        {
//            Game game = games.FirstOrDefault(g => g.Value.Players.Any(p => p.Id == connectionId)).Value;
//            if (game == null) return;

//            Player player = game.Players.Find(p => p.Id == connectionId);
//            game.Leave(player);

//            await hub.Groups.RemoveFromGroupAsync(hub.Context.ConnectionId, game.Id);
//            await hub.Clients.Group(game.Id).SendAsync(Constants.GAME_PLAYER_LEFT_CALLBACK, new PlayerLeftAction { GameId = game.Id, On = DateTime.Now, Player = player, ConnectionId = hub.Context.ConnectionId, Payload = game.Players, Lost = lost });
//        }
//    }
//}
