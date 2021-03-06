using System.Collections.Generic;

namespace LangrisserHelper.Shared
{
    public class PlayerLeftAction : GameActionBase
    {
        public new List<Player> Payload { get => base.Payload as List<Player>; set => base.Payload = value; }

        public bool Lost { get; set; }
    }
}
