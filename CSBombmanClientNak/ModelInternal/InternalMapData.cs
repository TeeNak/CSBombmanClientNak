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
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		public int MinX { get; set; }
		public int SizeX { get; set; }


		public int MinY { get; set; }

		public int SizeY { get; set; }

		public int Turn { get; set; }
		public List<Position> Walls { get; set; }
		public List<Position> Blocks { get; set; }
		public List<Player> Players { get; set; }
		public Player Me { get; set; }
		public List<Bomb> Bombs { get; set; }
		public List<Item> Items { get; set; }
		public List<Position> Fires { get; set; }

		public CellDict CellDict { get; set; }

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
			Me = mapData.Players.Find(p => p.Name == Consts.MyName);
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

			var ary2 = Enumerable.Range(MinY, SizeY).Select(y =>
			{
				return Enumerable.Range(MinX, SizeX).Select(x =>
				{
					var cell =new Cell()
					{
						X = x,
						Y = y,
						Wall = Walls.Any(w => w.x == x && w.y == y),
						Block = Blocks.Any(b => b.x == x && b.y == y),
						AnyPlayer = Players.Any(p => p.pos.x == x && p.pos.y == y && p.Name != Consts.MyName),
						MyPosition = Players.Any(p => p.pos.x == x && p.pos.y == y && p.Name == Consts.MyName),
						Bomb = Bombs.Find(b => b.pos.x == x && b.pos.y == y),
						Item = Items.Any(i => i.pos.x == x && i.pos.y == y),
						Fire = Fires.Any(f => f.x == x && f.y == y)
					};
					var pos = new Position(x, y);
					return new KeyValuePair<Position, Cell>(pos, cell);
				});
			}).SelectMany(e => e);

			CellDict = new CellDict(ary2, MinX, SizeX, MinY, SizeY);

			CellDict.SetDistance(Me.pos);
			CellDict.Log();
		}

		public IEnumerable<MOVE> PathToPosition(Position pos)
		{
			return CellDict.PathToPosition(pos);
		}


		public bool IsWallOrBlock(Position p)
		{
			var cell = CellDict[p];
			return cell.Wall || cell.Block;
		}


		public int ComputeThreatScore(Position pos)
		{

			var threats = Bombs.Select((b) =>
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

		public bool IsInDanger(Position pos)
		{
			return ComputeThreatScore(pos) > 0;
		}

		IEnumerable<Position> ReachablePosistions()
		{
			var ret = CellDict
				.Where(kv => kv.Value.Distance != Cell.UNREACHABLE)
				.Select(kv => kv.Key);
			return ret;
		}

		bool IsThereBlockWithinAffectedArea(Bomb bomb)
		{
			var affected = PosAffectedIncludingCausingExplosion(bomb);
			return affected.Any(p => CellDict[p].Block);
		}

		IEnumerable<Position> PosAffectedIncludingCausingExplosionInner(IEnumerable<Position> posBySingleExplosion, List<(Bomb, bool)> bps)
		{
			logger.Debug("---------- PosAffectedIncludingCausingExplosionInner start-----------");
			var ret = new HashSet<Position>();
			foreach(var pos in posBySingleExplosion)
			{
				var bomb = CellDict[pos].Bomb;
				if (bomb != null)
				{
					var tuple = bps.FirstOrDefault(bp => bp.Item1.pos.x == bomb.pos.x && bp.Item1.pos.y == bomb.pos.y);
					if(tuple.Item1 != null)
					{
						logger.Debug($"bomb {tuple.Item1.ToString()}");

						if (tuple.Item2)
						{
							// already processed
							break;
						}

						tuple.Item2 = true;

						var byItem1 = CellDict.PosAffectedBySingleBomb(tuple.Item1);
						var affectedByItem1 = PosAffectedIncludingCausingExplosionInner(byItem1, bps);
						foreach (var posByItem1 in affectedByItem1)
						{
							ret.Add(posByItem1);
						}
					}
				}

				ret.Add(pos);
			}
			logger.Debug("---------- PosAffectedIncludingCausingExplosionInner end-----------");
			return ret;
		}

		IEnumerable<Position> PosAffectedIncludingCausingExplosion(Bomb bomb)
		{
			logger.Debug("---------- PosAffectedIncludingCausingExplosion start-----------");

			var bps = Bombs.Select( b => (b, false)).ToList(); // (bomb, processed)

			var posBySingleExplosion = CellDict.PosAffectedBySingleBomb(bomb);

			var tuple = bps.FirstOrDefault(bp => bp.Item1.pos.x == bomb.pos.x && bp.Item1.pos.y == bomb.pos.y);
			if (tuple.Item1 != null)
			{
				tuple.Item2 = true; // processed
			}

			var pos = PosAffectedIncludingCausingExplosionInner(posBySingleExplosion, bps);

			logger.Debug("---------- PosAffectedIncludingCausingExplosion end -----------");
			return pos;
		}

		IEnumerable<Position> BreakableWhenBombPut()
		{
			var reachables = ReachablePosistions();

			var ret = reachables.Where(pos => 
			{
				var bomb = new Bomb(pos, Me.power);
				return IsThereBlockWithinAffectedArea(bomb);
			});

			logger.Debug("---------- BreakableWhenBombPut -----------");
			foreach (var r in ret)
			{
				logger.Debug(r.ToString);
			}
			logger.Debug("---------- -------------- -----------");

			return ret;
		}

		bool IsSafeToPutBomb(Position pos, int power)
		{

			logger.Debug("---------- IsSafeToPutBomb -----------");
			logger.Debug($"pos {pos.ToString()} power {power} ");

			// 置こうとしている場所が、既存の爆弾の誘爆圏内ならfalse
			var isInExplosion = Bombs.Any(bomb =>
			{
				var affects = PosAffectedIncludingCausingExplosion(bomb);
				return affects.Contains(pos);
			});

			if (isInExplosion)
			{
				logger.Debug($"pos {pos.ToString()} is inExplosion ");
				logger.Debug($"false ");
				logger.Debug("---------- IsSafeToPutBomb -----------");
				return false;
			}

			// 置いた場合5歩以内に誘爆圏内から逃れられるか？
			var b = new Bomb(pos, power);
			var affected = PosAffectedIncludingCausingExplosion(b);

			var minEscapeStep = affected.Select(p =>
			{
				return CellDict[p].Distance;
			}).Min();

			logger.Debug($"minEscapeStep {minEscapeStep} ");
			if (minEscapeStep > 5)
			{
				logger.Debug($"false ");
				logger.Debug("---------- IsSafeToPutBomb -----------");
				return false;
			}

			logger.Debug($"true ");
			logger.Debug("---------- IsSafeToPutBomb -----------");
			return true;
		}

		public IEnumerable<Position> PlaceToSetBomb()
		{
			var breakables = BreakableWhenBombPut();

			var ret = breakables.Where(pos =>
			{
				return IsSafeToPutBomb(pos, Me.power);
			}).ToList();

			logger.Debug("---------- PlaceToSetBomb -----------");
			foreach (var r in ret)
			{
				logger.Debug(r.ToString);
			}
			logger.Debug("---------- -------------- -----------");

			return ret;
		}

		public Position FindNearestSafePlace()
		{
			var minDist = CellDict
				.Where(kv => !IsInDanger(kv.Key) )
				.Select(kv => kv.Value.Distance)
				.Min();

			logger.Debug("---------- FindNearestSafePlace -----------");
			logger.Debug($"minDist: {minDist}");

			if (minDist == Cell.UNREACHABLE)
			{
				// 死んだ
				return null;
			}

			var p = CellDict
				.Where(kv => kv.Value.Distance == minDist)
				.Select(kv => kv.Key)
				.First();

			logger.Debug($"pos: {p.ToString()}");
			logger.Debug("---------- FindNearestSafePlace -----------");
			return p;
		}

		public void AddBomb(Bomb bomb)
		{
			Bombs.Add(bomb);
			CellDict[bomb.pos].Bomb = bomb;
		}
	}
}
