using CSBombmanClientNak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanServer
{
    public class Position : IEquatable<Position>
    {
        public int x { get; set; }
        public int y { get; set; }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

		public Position PositionAfterMove(MOVE m)
		{
			Position ret;
			switch (m)
			{
				case MOVE.UP:
					ret = new Position(x, y - 1);
					break;
				case MOVE.DOWN:
					ret = new Position(x, y + 1);
					break;
				case MOVE.LEFT:
					ret = new Position(x - 1, y);
					break;
				case MOVE.RIGHT:
					ret = new Position(x + 1, y);
					break;
				case MOVE.STAY:
				default:
					ret = new Position(x, y);
					break;

			}
			return ret;
		}

		public Position Clone()
		{
			return new Position(x, y);
		}

		public static bool operator ==(Position obj1, Position obj2)
		{
			if (ReferenceEquals(obj1, obj2))
			{
				return true;
			}

			if (ReferenceEquals(obj1, null))
			{
				return false;
			}
			if (ReferenceEquals(obj2, null))
			{
				return false;
			}

			return (obj1.x == obj2.x
					&& obj1.y == obj2.y);
		}

		// this is second one '!='
		public static bool operator !=(Position obj1, Position obj2)
		{
			return !(obj1 == obj2);
		}


		public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Position return false.
            Position p = obj as Position;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y == p.y);
        }

        public bool Equals(Position p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y == p.y);
        }

        public override int GetHashCode()
        {
            return x ^ y;
        }

        public override string ToString()
        {
            return $"[{x},{y}]";
        }
    }
}
