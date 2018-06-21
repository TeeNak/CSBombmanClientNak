using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSBombmanServer
{
    public class Player
    {
        public const int DEFAULT_POWER = 2;
        public const int DEFAULT_BOMB_LIMIT = 2;
        [JsonProperty("name", Order = 1)]
        public string Name { get; set; }
        [JsonProperty(Order = 2)]
        public Position pos;
        [JsonProperty(Order = 3)]
        public int power;
        [JsonProperty(Order = 4)]
        public int setBombLimit;
        [JsonProperty(Order = 5)]
        public char ch;
        [JsonProperty(Order = 6)]
        public bool isAlive;
        [JsonProperty(Order = 7)]
        public int setBombCount;
        [JsonProperty(Order = 8)]
        public int totalSetBombCount;
        [JsonProperty("id", Order = 9)]
        public int Id;

        public virtual void SetId(int value)
        {
            this.Id = value;
        }

        public Player(string name)
        {
            this.Name = name;
            this.power = DEFAULT_POWER;
            this.setBombLimit = DEFAULT_BOMB_LIMIT;
            this.ch = name.ToCharArray()[0];
            this.isAlive = true;
            this.setBombCount = 0;
        }

        public bool CanSetBomb()
        {
            return setBombCount < setBombLimit;
        }

        public async virtual Task<ActionData> Action(string mapdata)
        {
            return new ActionData(this, "STAY", false);
        }

        public virtual void Dispose()
        {
        }
    }
}
