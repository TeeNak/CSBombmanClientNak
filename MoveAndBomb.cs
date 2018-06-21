using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanClientNak
{
	public enum MOVE { UP, DOWN, LEFT, RIGHT, STAY };

	public class MoveAndBomb
	{
		public MOVE Move { get; set; }
		public bool Bomb { get; set; }

		public static string MoveToString(MOVE move)
		{
			string ret = "UP";
			switch (move)
			{
				case MOVE.UP:
					ret = "UP";
					break;
				case MOVE.DOWN:
					ret = "DOWN";
					break;
				case MOVE.LEFT:
					ret = "LEFT";
					break;
				case MOVE.RIGHT:
					ret = "RIGHT";
					break;
				case MOVE.STAY:
					ret = "STAY";
					break;

			}
			return ret;
		}

		public string ToCommandString()
		{
			return MoveToString(Move) + "," + Bomb.ToString();
		}
	}
}
