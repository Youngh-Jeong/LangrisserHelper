using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LangrisserHelper.Shared;
using Microsoft.AspNetCore.SignalR;

namespace LangrisserHelper.Server.Engine
{
    public static class ThreadSafeRandom
    {
        private static readonly Random _global = new Random();
        [ThreadStatic] private static Random _local;

        public static int Next(int min, int max)
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        int seed = _global.Next(min, max);
                        _local = new Random(seed);
                    }
                }
            }

            return _local.Next(min, max);
        }
    }

    public class Game
    {
        //static Random random = new Random(DateTime.Now.Ticks.GetHashCode());
        public Game(string id, Player player)
        {
            Id = id;
            Players = new List<Player>();
            Join(player);
            NewEgg();
            Map = GetRandomMap();
        }

        public string Id { get; private set; }
        public List<Player> Players { get; private set; }
        public Point Egg { get; set; }
        public int Map { get; set; }



        public Point NewEgg()
        {
            Egg = GetRandomLocation();//new Point(30, 50);//
            return Egg;
        }

        private int GetRandomMap() 
        {
            return 0;
        }
        private Point GetRandomLocation()
        {
            // not any random x or y inside canvas, the size of the snake point has to be taken into account
            // for example if snake point size is 10 we dont want to get any random x between 0 or 10 but either 0, 10, or 20, etc..
            var xRange = Enumerable.Range(Constants.SNAKE_SIZE, Constants.CANVAS_WIDTH).Where(x => x % Constants.SNAKE_SIZE == 0).ToList();
            var yRange = Enumerable.Range(Constants.SNAKE_SIZE, Constants.CANVAS_HEIGHT).Where(x => x % Constants.SNAKE_SIZE == 0).ToList();
            int xIndex = ThreadSafeRandom.Next(0, xRange.Count - 1);
            int yIndex = ThreadSafeRandom.Next(0, yRange.Count - 1);

            Point randomLocation = new Point(xRange[xIndex], yRange[yIndex]);
            if (!IsValidLocation(randomLocation))
            {
                return GetRandomLocation();
            }
            return randomLocation;
        }
        private bool IsValidLocation(Point locaiton)
        {
            return true;
            //return !Players.Any(p => p.Snake.Position.Any(s => s.Equals(locaiton)));
        }

        public void Join(Player player)
        {
            if (!Players.Any(p => p.Id == player.Id))
            {
                Point randomLocation = GetRandomLocation();

                var position = new LinkedList<Point>();
                position.AddLast(new LinkedListNode<Point>(randomLocation));
                player.Roaster = new List<int>(); // 리스트 불러와서 저장할 것
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
    }
}
