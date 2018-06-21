using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanServer
{
    public class Block
    {
        public Position pos;
        [NonSerialized]
        private Item item;

        public Item GetItem()
        {
            return item;
        }

        public void SetItem(Item item)
        {
            this.item = item;
        }

        public Block(Position pos)
        {
            this.pos = pos;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Block return false.
            Block b = obj as Block;
            if ((System.Object)b == null)
            {
                return false;
            }

            // Return true if the fields match:
            return pos.Equals(b.pos);
        }

        public bool Equals(Block b)
        {
            // If parameter is null return false:
            if ((object)b == null)
            {
                return false;
            }

            // Return true if the fields match:
            return pos.Equals(b.pos);
        }

        public override int GetHashCode()
        {
            return pos.GetHashCode();
        }
    }
}
