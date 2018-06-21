using CSBombmanServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanClientNak
{
	class MoveDecider
	{

		static bool InWall(Position pos, List<int[]> walls)
		{
			return walls.Exists(wall => wall[0] == pos.x && wall[1] == pos.y);
		}

		static bool InBlock(Position pos, List<int[]> blocks)
		{
			return blocks.Exists(wall => wall[0] == pos.x && wall[1] == pos.y);
		}

/*
		static MoveAndBomb NextMove(MapData map)
		{
			var result = new MoveAndBomb();

			var p = map.Players.Find(player => player.Name.Contains(MyName));

			var allMoves = new MOVE[] { MOVE.UP, MOVE.DOWN, MOVE.LEFT, MOVE.RIGHT, MOVE.STAY }.ToList();

			var availableMoves =
					allMoves
					.Where(move => {
						return !InWall(CalcPos(p.pos, move), map.Walls);
					})
					.Where(move => {
						return !InBlock(CalcPos(p.pos, move), map.Blocks);
					})
					.Where(move =>
					{
						return ComputeThreatScore(CalcPos(p.pos, move), map.Bombs) == 0;
					});

			if (availableMoves.Count() == 0)
			{
				// やけくそ移動
				availableMoves = allMoves;
			}

			result.Move = ChooseRandomMove(availableMoves.ToList());
			result.Bomb = false;

			return result;

		}
*/
	}
}
