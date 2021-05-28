using System;
using System.Collections.Generic;

namespace LangrisserHelper.Shared
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Face { get; set; }
        public DateTime JoinedOn { get; set; }
        public List<int> Roaster { get; set; } = new List<int>(15);
        public List<int> Banned { get; set; } = new List<int>();
        public List<int> Picked { get; set; } = new List<int>();
        public List<int> Rest { get; set; } = new List<int>();
        public bool IsFirst { get; set; }
        public bool IsReady { get; set; }
        public void Ban(List<int> banList) 
        {
            foreach (var banId in banList) 
            {
                Banned.Add(banId);
            }
        }
        public void Pick(List<int> pickList) 
        {
            foreach (var pickId in pickList) 
            {
                Picked.Add(pickId);            
            }
            if (pickList.Count == 0)
            {
                Random random = new Random();
                int randomId = 0;
                do
                {
                    randomId = random.Next(0,15);
                }
                while (Banned.Contains(Roaster[randomId]));
                Picked.Add(Roaster[randomId]);
            }
        }
    }
}
