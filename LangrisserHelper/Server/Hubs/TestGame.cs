using LangrisserHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LangrisserHelper.Server.Hubs
{
    public class TestGame
    {
        //static Random random = new Random(DateTime.Now.Ticks.GetHashCode());
        public TestGame(string id, Player player)
        {
            Id = id;
            Players = new List<Player> { player};
            Map = GetRandomMap();
        }

        public string Id { get; private set; }
        public List<Player> Players { get; private set; } = new List<Player>();
        public int Map { get; set; }
        public bool CanJoin { get { return (Players == null || Players.Count<2); }  }
        public Player Me(string player) 
        {
            return Players.Where(p => p.Id == player).FirstOrDefault();
        }
        public Player Opponent(string player) 
        {
            return Players.Where(p => p.Id != player).FirstOrDefault();
        }
        public async Task<int> SetRandomMap() 
        {
            Random random = new Random();
            Map = random.Next(0, 11);
            await Task.Delay(1);
            return Map;
        }
        public async Task<string> SetFirst(string id) 
        {
            Random random = new Random();
            int flag = random.Next(0, 2);
            if (flag == 0)
            {
                Me(id).IsFirst = true;
                return Me(id).Name;
            }
            else 
            {
                Opponent(id).IsFirst = true;
                return Opponent(id).Name;
            }
        }
        private int GetRandomMap()
        {
            return 0;
        }

        public void Join(Player player)
        {
            if (!Players.Any(p => p.Id == player.Id))
            {
                player.Face = "1";
                
                Players.Add(player);
            }
        }

        public void Leave(Player player)
        {
            if (Players.Any(p => p.Id == player.Id))
            {
                Players.Remove(Players.First(p => p.Id == player.Id));
            }
        }
        public async Task LeaveAsync(string player)
        {
            bool HasPlayer = await Task.Run(() => { return Players.Any(p => p.Id == player); });
            if (HasPlayer)
            {
                await Task.Run(() => { Players.Remove(Players.First(p => p.Id == player)); });
            }
        }
    }

}
