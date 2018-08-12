using CSBombmanClientNak.ModelInternal;
using CSBombmanServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanClientNak
{
	class ActionDecider
	{
		static MOVE ChooseRandomMove(List<MOVE> possible)
		{
			var rand = new Random();

			int i = rand.Next(0, possible.Count);

			return possible[i];
		}


		static int ComputeThreatScore(Position pos, List<Bomb> bombs)
		{

			var threats = bombs.Select((b) =>
			{
				if (pos.x - b.pos.x == 0 && pos.y - b.pos.y <= b.power)
				{
					return new Threat { ThreatLevel = ThreatLevel.Danger, Direction = Direction.North };
				}
				else if (pos.x - b.pos.x == 0 && b.pos.y - pos.y <= b.power)
				{
					return new Threat { ThreatLevel = ThreatLevel.Danger, Direction = Direction.South };
				}
				else if (pos.y - b.pos.y == 0 && pos.x - b.pos.x <= b.power)
				{
					return new Threat { ThreatLevel = ThreatLevel.Danger, Direction = Direction.West };
				}
				else if (pos.y - b.pos.y == 0 && b.pos.x - pos.x <= b.power)
				{
					return new Threat { ThreatLevel = ThreatLevel.Danger, Direction = Direction.East };
				}

				return new Threat { ThreatLevel = ThreatLevel.None, Direction = Direction.None };
			}).Where((threat) => threat.ThreatLevel != ThreatLevel.None);

			return threats.Count();
		}


		static Position CalcPos(Position pos, MOVE move)
		{
			if (move == MOVE.UP)
			{
				return new Position(pos.x, pos.y - 1);
			}
			else if (move == MOVE.DOWN)
			{
				return new Position(pos.x, pos.y + 1);
			}
			else if (move == MOVE.LEFT)
			{
				return new Position(pos.x - 1, pos.y);
			}
			else if (move == MOVE.RIGHT)
			{
				return new Position(pos.x + 1, pos.y);
			}
			else
			{
				return new Position(pos.x, pos.y);
			}
		}

		static bool InWall(Position pos, List<Position> walls)
		{
			return walls.Exists(wall => wall.x == pos.x && wall.y == pos.y);
		}

		static bool InBlock(Position pos, List<Position> blocks)
		{
			return blocks.Exists(wall => wall.x == pos.x && wall.y == pos.y);
		}

		public Action NextMove(InternalMapData map)
		{
			var result = new Action();

			var p = map.Players.Find(player => player.Name.Contains(Consts.MyName));

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

	}
}
