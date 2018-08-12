using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSBombmanServer
{
    public class Bomb
    {
        public const int EXPLODE_TIMER = 10;
        public Position pos;
        public int timer;
        public int power;
        [NonSerialized]
        public Player owner;

        public Bomb(Player owner)
        {
            this.pos = owner.pos; // pos ha immutable
            this.power = owner.power;
            this.timer = EXPLODE_TIMER;
            this.owner = owner;
        }

        [JsonConstructor]
        public Bomb(Position pos, int timer, int power)
        {
            this.pos = pos; // pos ha immutable
            this.power = power;
            this.timer = EXPLODE_TIMER;
            this.owner = null;
        }

        public override string ToString()
        {
            return "[" + pos.x + "," + pos.y + "]";
        }
    }

}
