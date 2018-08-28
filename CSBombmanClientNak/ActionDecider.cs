using CSBombmanClientNak.ModelInternal;
using CSBombmanServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanClientNak
{

	public class ActionDecider
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		static MOVE ChooseRandomMove(List<MOVE> possible)
		{
			var rand = new Random();

			int i = rand.Next(0, possible.Count);

			return possible[i];
		}



		public Action NextMove(InternalMapData map)
		{
			logger.Debug("*** NextMove start *************");

			var result = new Action();

			var p = map.Players.Find(player => player.Name.Contains(Consts.MyName));

			var allMoves = new MOVE[] { MOVE.UP, MOVE.DOWN, MOVE.LEFT, MOVE.RIGHT, MOVE.STAY }.ToList();

			var availableMoves =
					allMoves
					.Where(move => {
						return !map.IsWallOrBlock(p.pos.PositionAfterMove(move));
					})
					;

			if (availableMoves.Count() == 0)
			{
				// 閉じ込められた？
				logger.Debug("No Choice.");

				availableMoves = allMoves;
				result.Move = ChooseRandomMove(availableMoves.ToList());
				result.Bomb = false;
				return result;
			}

			if (map.IsInDanger(p.pos))
			{
				// in danger
				logger.Debug("in danger");

				Position nearestSafePos = map.FindNearestSafePlace();
				if(nearestSafePos == null)
				{
					// 死んだ。やけくそ移動

					logger.Debug("I am dying.");

					result.Move = ChooseRandomMove(availableMoves.ToList());
					result.Bomb = false;
					return result;
				}

				var pathToSafePlace = map.PathToPosition(nearestSafePos);
				if(pathToSafePlace.Count() == 0)
				{
					result.Move = ChooseRandomMove(availableMoves.ToList());
					result.Bomb = false;
					return result;
				}

				result.Move = pathToSafePlace.FirstOrDefault();
				result.Bomb = false;
				return result;

			}
			else
			{
				// not in danger
				logger.Debug("not in danger");

				var placeToSetBombs = map.PlaceToSetBomb().ToList();

				foreach( var placeToSetBomb in placeToSetBombs)
				{
					if (placeToSetBomb == p.pos)
					{
						var tempBomb = new Bomb(p);
						map.AddBomb(tempBomb);

						Position nearestSafePos = map.FindNearestSafePlace();

						map.RemoveBomb(tempBomb);

						if (nearestSafePos == null)
						{
							// 置くと安全な場所がない
							continue;
						}

						var pathToSafePlace = map.PathToPosition(nearestSafePos);

						result.Move = pathToSafePlace.FirstOrDefault();
						result.Bomb = true;
						return result;
					}
					else
					{
						var pathToPlaceBomb = map.PathToPosition(placeToSetBomb);

						var move = pathToPlaceBomb.FirstOrDefault();

						if(map.IsInDanger(p.pos.PositionAfterMove(move)))
						{
							// 次の場所へ
							continue;
						}

						result.Move = move;
						result.Bomb = false;
						return result;

					}
				}

				var safeMoves = availableMoves.Where(move =>
				{
					return !map.IsInDanger(p.pos.PositionAfterMove(move));
				});

				if (safeMoves.Count() == 0)
				{
					// やけくそ
					logger.Debug("I am dying 2.");
					result.Move = availableMoves.First();
					result.Bomb = false;
					return result;
				}


				result.Move = ChooseRandomMove(safeMoves.ToList());
				result.Bomb = false;
				return result;
			}


		}

	}
}
