using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using LangrisserHelper.Shared;

namespace LangrisserHelper.Client
{
    public class GameClient : IAsyncDisposable
    {
        #region PRIVATE FIELDS
        private readonly string _hubUrl;
        private HubConnection _hubConnection;
        private readonly string gameId;
        public readonly Player player;

        public Player opponent;
        public int Turn;
        public int MapId;
        #endregion

        public string OpName {get{ if (opponent == null) return ""; return opponent.Name; } }




        public event MessageArrivedEventHandler MessageArrived; // 시스템 메세지(나중엔 유저메세지도?)가 왔을 때 튀는 이벤트
        public event OpponentChangedHandler OpponentChanged; // 상대에 대한 정보를 받았을 때 (상대가 변경되었을 때) 튀는 이벤트
        public event OpponentChangedHandler MeChanged; // 나 대한 정보를 받았을 때 (밴 등 상대가 조작) 튀는 이벤트
        public event ResetBaseTimerHandler ResetTimer; // 타이머를 45초로 리셋해야 할 때 튀는 이벤트
        public event OpponentReadyChangedHandler OpponentReadyChanged;
        public event StartGame StartGameEvent;
        public event EndGame EndGameEvent;
        public event TurnChange TurnChanged;

        public delegate void MessageArrivedEventHandler(string text);
        public delegate void OpponentChangedHandler(Player opponent);
        public delegate void MeChangedHandler(Player me);
        public delegate void OpponentReadyChangedHandler(bool IsReady);
        public delegate void StartGame();
        public delegate void EndGame();
        public delegate void TurnChange(int i);
        public delegate void ResetBaseTimerHandler();
        #region CONSTRUCTOR
        public GameClient(string gameId, Player player, string siteUrl)
        {
            if (string.IsNullOrWhiteSpace(gameId))
                throw new ArgumentNullException(nameof(gameId));
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            if (string.IsNullOrWhiteSpace(siteUrl))
                throw new ArgumentNullException(nameof(siteUrl));

            this.gameId = gameId;
            this.player = player;
            _hubUrl = siteUrl.TrimEnd('/') + "/RoomTest";
        }
        #endregion

        #region PUBLIC PROPERTIES
        public bool Connected { get => _hubConnection?.State == HubConnectionState.Connected; }
        public string ConnectionId { get => _hubConnection?.ConnectionId; }
        #endregion

        #region EVENTS
        public event MessageReceivedEventHandler MessageReceived;
        public event StateChangedEventHandler StateChanged;
        public event NewEggFoundEventHandler NewEggFound;
        #endregion

        public void SetOpponent(Player opp)
        {
            opponent = opp;
            OpponentChanged.Invoke(opp);
        }
        public void SetMe(Player me)
        {
            player.Banned = me.Banned;
            OpponentChanged.Invoke(me);
        }
        public async Task EnterAsync() 
        {
            if (_hubConnection == null)
            {
                _hubConnection = new HubConnectionBuilder().WithUrl(_hubUrl).Build();
                _hubConnection.ServerTimeout = TimeSpan.FromHours(1);
                _hubConnection.Closed += _hubConnection_Closed;
                _hubConnection.Reconnected += _hubConnection_Reconnected;
                _hubConnection.Reconnecting += _hubConnection_Reconnecting;
                
                _hubConnection.On<string>("Send", (args =>MessageArrived.Invoke(args)));
                _hubConnection.On<Player>("OpponentChanged", SetOpponent);
                _hubConnection.On<Player>("MeChanged", SetMe);
                _hubConnection.On<int>("TurnChanged", (args => { Turn++; if (Turn < 21) TurnChanged.Invoke(Turn); else EndGameEvent.Invoke(); })); // 끝날 턴 결정
                _hubConnection.On<bool>("OpponentReadyChanged", (args => { opponent.IsReady = args; OpponentChanged.Invoke(opponent); }));
                _hubConnection.On<int, string>("GameStart", ((map, firstUserName) => { 
                    MapId = map;
                    if (player.Name == firstUserName)
                    {
                        player.IsFirst = true;
                    }
                    else 
                    {
                        opponent.IsFirst = true;
                    }
                    Turn = 1;
                    StartGameEvent.Invoke(); 
                }));
                // T: Handler가 받는 변수, 앞의 것이 invoke되면 뒤의것이 실행됨

                await _hubConnection.StartAsync();

                await _hubConnection.SendAsync("AddToGroup", gameId, player);
            }
            else if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                await _hubConnection.StartAsync();

                await _hubConnection.SendAsync("AddToGroup", gameId, player);
            }
        }
        public async Task LeaveAsync() 
        {
            if (_hubConnection != null && _hubConnection.State != HubConnectionState.Disconnected)
            {
                await _hubConnection.SendAsync("RemoveFromGroup", gameId, player);
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }
        public async Task ReadyAsync(bool IsReady) 
        {
            player.IsReady = IsReady;
            if (_hubConnection != null && _hubConnection.State != HubConnectionState.Disconnected)
            {
                await _hubConnection.SendAsync("Ready", gameId,IsReady);
            }
        }

        public async Task PickAsync(List<int> selectedList)
        {
            if (_hubConnection != null && _hubConnection.State != HubConnectionState.Disconnected)
            {
                player.Pick(selectedList);
                await _hubConnection.SendAsync("Picked",gameId, selectedList);
            }
        }
        public async Task BanAsync(List<int> selectedList)
        {
            if (_hubConnection != null && _hubConnection.State != HubConnectionState.Disconnected)
            {
                opponent.Ban(selectedList);
                await _hubConnection.SendAsync("Banned",gameId, selectedList);
            }
        }
        public async Task SendMessageAsync(string msg) 
        {
            await _hubConnection.SendAsync("Message", gameId, msg);
        }
        #region PUBLIC METHODS
        public async Task StartAsync()
        {
            if (_hubConnection == null)
            {
                _hubConnection = new HubConnectionBuilder().WithUrl(_hubUrl).Build();
                _hubConnection.ServerTimeout = TimeSpan.FromHours(1);
                _hubConnection.Closed += _hubConnection_Closed;
                _hubConnection.Reconnected += _hubConnection_Reconnected;
                _hubConnection.Reconnecting += _hubConnection_Reconnecting;

                _hubConnection.On<PlayerJoinedAction>(Constants.GAME_PLAYER_JOINED_CALLBACK, HandleGameActionAsync);
                _hubConnection.On<PlayerLeftAction>(Constants.GAME_PLAYER_LEFT_CALLBACK, HandleGameActionAsync);
                _hubConnection.On<PlayerPlayedAction>(Constants.GAME_ACTION_CALLBACK, HandleGameActionAsync);
                _hubConnection.On<Point>(Constants.GAME_NEW_EGG, HandleNewEggFoundAsync);
                // T: Handler가 받는 변수, 앞의 것이 invoke되면 뒤의것이 실행됨

                await _hubConnection.StartAsync();

                await _hubConnection.SendAsync(Constants.GAME_PLAYER_JOINED, new PlayerJoinedAction { GameId = gameId, Payload = new List<Player> { player }, ConnectionId = _hubConnection.ConnectionId, On = DateTime.Now });
            }
            else if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                await _hubConnection.StartAsync();
                await _hubConnection.SendAsync(Constants.GAME_PLAYER_JOINED, new PlayerJoinedAction { GameId = gameId, Payload = new List<Player> { player }, ConnectionId = _hubConnection.ConnectionId, On = DateTime.Now });
            }
        }

        public async Task PlayAsync(Play play)
        {
            if (_hubConnection.State == HubConnectionState.Connected)
            {
                await _hubConnection.SendAsync(Constants.GAME_ACTION, new PlayerPlayedAction { GameId = gameId, Payload = play, ConnectionId = _hubConnection.ConnectionId, On = DateTime.Now });
            }
        }

        public async Task StopAsync()
        {
            if (_hubConnection != null && _hubConnection.State != HubConnectionState.Disconnected)
            {
                await _hubConnection.SendAsync(Constants.GAME_PLAYER_LEFT, new PlayerLeftAction { GameId = gameId, Payload = new List<Player> { player }, ConnectionId = _hubConnection.ConnectionId, On = DateTime.Now });
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            await LeaveAsync();
        }
        #endregion

        #region EVENT HANDLERS
        private async Task _hubConnection_Reconnecting(Exception arg)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(HubConnectionState.Reconnecting, arg?.Message));
        }

        private async Task _hubConnection_Reconnected(string arg)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(HubConnectionState.Connected, arg));
        }

        private async Task _hubConnection_Closed(Exception arg)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(HubConnectionState.Disconnected, arg?.Message));
        }

        private async Task HandleGameActionAsync(IGameAction action)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(action));

            // await Task.Factory.FromAsync( ( asyncCallback, @object ) => 
            // this.MessageReceived.BeginInvoke( this, new MessageReceivedEventArgs(action), asyncCallback, @object ), this.MessageReceived.EndInvoke, null );
        }

        private async Task HandleNewEggFoundAsync(Point egg)
        {
            NewEggFound?.Invoke(this, new NewEggFoundEventArgs(egg));
        }
        #endregion
    }

    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);
    public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs e);
    public delegate void NewEggFoundEventHandler(object sender, NewEggFoundEventArgs e);

    public class NewEggFoundEventArgs : EventArgs
    {
        public NewEggFoundEventArgs(Point egg)
        {
            Egg = egg;
        }

        public Point Egg { get; private set; }

    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(IGameAction action)
        {
            Action = action;
        }

        public IGameAction Action { get; private set; }

    }

    public class StateChangedEventArgs : EventArgs
    {
        public StateChangedEventArgs(HubConnectionState state, string message)
        {
            this.State = state;
            this.Message = message;
        }

        public HubConnectionState State { get; private set; }
        public string Message { get; private set; }
    }
}
