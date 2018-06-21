using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanServer
{
    public class ActionData
    {
        public Player p;
        public string dir;
        public bool putBomb;
        // for debug?
        public string message = "";

        public ActionData(Player p, string dir, bool putBomb)
        {
            this.p = p;
            this.dir = dir;
            this.putBomb = putBomb;
        }

        public ActionData(Player p, string dir, bool putBomb, string message)
        {
            this.p = p;
            this.dir = dir;
            this.putBomb = putBomb;
            this.message = message;
        }

        public override string ToString()
        {
            return p.Name + ": " + dir + "," + putBomb;
        }
    }
}
