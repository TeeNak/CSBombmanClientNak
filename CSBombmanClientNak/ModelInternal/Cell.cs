using CSBombmanServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanClientNak.ModelInternal
{
	public class Cell
	{
		public const int UNREACHABLE = 99999;

		public int X { get; set; }
		public int Y { get; set; }

		public bool Wall { get; set; }
		public bool Block { get; set; }

		/// <summary>
		/// 誰かそこにいるか
		/// </summary>
		public bool AnyPlayer { get; set; }

		public bool MyPosition { get; set; }

		public Bomb Bomb { get; set; }

		public bool Item { get; set; }

		public bool Fire { get; set; }

		public int Distance { get; set; } = UNREACHABLE;

		public List<MOVE> Path { get; set; }

		public string DebugString()
		{
			if (Wall)
			{
				return "■";
			}
			else if (Block)
			{
				return "□";
			}
			else if (Bomb != null)
			{
				return $"B{Bomb.power}";
			}
			else if (Fire)
			{
				return "火";
			}
			else if (Item)
			{
				return "＊";
			}
			else if (AnyPlayer)
			{
				return "Ｐ";
			}
			else if (MyPosition)
			{
				return "自";
			}
			return "　";
		}

		public string DebugStringWithDistance()
		{
			if (Distance != UNREACHABLE)
			{
				return $"{Distance:D2}";
			}
			else
			{
				return DebugString();
			}
		}

	}
}
