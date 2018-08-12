using CSBombmanServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanClientNak.ModelInternal
{
	public class InternalMapData
	{
		public int Turn { get; set; }
		public List<Position> Walls { get; set; }
		public List<Position> Blocks { get; set; }
		public List<Player> Players { get; set; }
		public List<Bomb> Bombs { get; set; }
		public List<Item> Items { get; set; }
		public List<int[]> Fires { get; set; }


		public InternalMapData(MapData mapData)
		{
			FromMapData(mapData);
		}

		public void FromMapData(MapData mapData)
		{
			Turn = mapData.Turn;
			Walls = mapData.Walls.Select(x => new Position(x[0], x[1])).ToList();
			Blocks = mapData.Blocks.Select(x => new Position(x[0], x[1])).ToList();
			Players = mapData.Players;
			Bombs = mapData.Bombs;
			Items = mapData.Items;
			Fires = mapData.Fires;
		}
	}
}
