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
		public bool Wall { get; set; }
		public bool Block { get; set; }

		/// <summary>
		/// 誰かそこにいるか
		/// </summary>
		public bool AnyPlayer { get; set; }

		public bool Bomb { get; set; }

		public bool Item { get; set; }

		public bool Fire { get; set; }
	}


	public class InternalMapData
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		public int MinX { get; set; }
		public int SizeX { get; set; }


		public int MinY { get; set; }

		public int SizeY { get; set; }

		public int Turn { get; set; }
		public List<Position> Walls { get; set; }
		public List<Position> Blocks { get; set; }
		public List<Player> Players { get; set; }
		public List<Bomb> Bombs { get; set; }
		public List<Item> Items { get; set; }
		public List<Position> Fires { get; set; }


		public InternalMapData(MapData mapData)
		{
			FromMapData(mapData);
		}

		public void FromMapData(MapData mapData)
		{
			Turn = mapData.Turn;
			Walls = mapData.Walls.Select(item => new Position(item[0], item[1])).ToList();
			Blocks = mapData.Blocks.Select(item => new Position(item[0], item[1])).ToList();
			Players = mapData.Players;
			Bombs = mapData.Bombs;
			Items = mapData.Items;
			Fires = mapData.Fires.Select(item => new Position(item[0], item[1])).ToList();

			MinX = Walls.Select(item => item.x).Min();
			int MaxX = Walls.Select(item => item.x).Max();
			SizeX = MaxX - MinX + 1;

			MinY = Walls.Select(item => item.y).Min();
			int MaxY = Walls.Select(item => item.y).Max();
			SizeY = MaxY - MinY + 1;

			logger.Debug($"MinX: {MinX}, SizeX: {SizeX}, MinY {MinY}, SizeY {SizeY}");

		}

		/// <summary>
		/// Position が移動可能か
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		bool IsMovableTo(Position p)
		{
			return true;
		}
	}
}
