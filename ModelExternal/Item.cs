using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanServer
{
    public class Item
    {
        public Position pos;
        public char name;

        public Item(char name, Position pos)
        {
            this.pos = pos;
            this.name = name;
        }

        // アイテムによって振る舞いを変えたいときは
        // powerClassとかTamaClassとか作るべきなのかもしれないが
        public void Effect(Player p)
        {
            if (this.name == '力')
            {
                p.power += 1;
            }
            else if (this.name == '弾')
            {
                p.setBombLimit += 1;
            }
        }
    }
}
