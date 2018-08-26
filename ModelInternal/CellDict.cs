using CSBombmanServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanClientNak.ModelInternal
{
	public class CellDict : Dictionary<Position, Cell>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		private int MinX { get; }
		private int SizeX { get; }
		private int MinY { get; }
		private int SizeY { get; }

		public CellDict(IEnumerable<KeyValuePair<Position, Cell>> _kvEnumerable, int _MinX, int _SizeX, int _MinY, int _SizeY)
		{
			foreach (var kv in _kvEnumerable)
			{
				this[kv.Key] = kv.Value;
			}
			MinX = _MinX;
			SizeX = _SizeX;
			MinY = _MinY;
			SizeY = _SizeY;
		}
		public void Log()
		{
			logger.Debug("CellDict -------------");

			foreach (var y in Enumerable.Range(MinY, SizeY))
			{
				string rowString = "";
				foreach (var x in Enumerable.Range(MinX, SizeX))
				{
					var cell = this[new Position(x, y)];
					rowString += cell.DebugStringWithDistance();
				}
				logger.Debug(rowString);
			}
			logger.Debug("-----------------------");
		}

		private void SetDistanceInner(Position pos, Stack<MOVE> path, int distance)
		{

			var cell = this[pos];
			if (cell.Wall || cell.Block || cell.Fire || cell.Bomb != null)
			{
				return;
			}

			if (cell.Distance == Cell.UNREACHABLE || cell.Distance > distance)
			{
				cell.Distance = distance;
				if(distance == 0)
				{
					var list = new List<MOVE>();
					list.Add(MOVE.STAY);
					cell.Path = list;
				}
				else
				{
					cell.Path = path.Reverse().Select(m => m).ToList();
				}



				path.Push(MOVE.UP);
				SetDistanceInner(pos.PositionAfterMove(MOVE.UP), path, distance + 1);
				path.Pop();

				path.Push(MOVE.DOWN);
				SetDistanceInner(pos.PositionAfterMove(MOVE.DOWN), path, distance + 1);
				path.Pop();

				path.Push(MOVE.LEFT);
				SetDistanceInner(pos.PositionAfterMove(MOVE.LEFT), path, distance + 1);
				path.Pop();

				path.Push(MOVE.RIGHT);
				SetDistanceInner(pos.PositionAfterMove(MOVE.RIGHT), path, distance + 1);
				path.Pop();
			}
		}

		public void SetDistance(Position pos)
		{
			var path = new Stack<MOVE>();
			SetDistanceInner(pos, path, 0);
		}

		public IEnumerable<MOVE> PathToPosition(Position pos)
		{
			return this[pos].Path;
		}


		public IEnumerable<Position> PosAffectedBySingleBomb(Bomb bomb)
		{

			Position inPos = bomb.pos;
			int power = bomb.power;

			yield return inPos; // 爆心地

			for (var i = 1; i <= power; i++)
			{
				if(inPos.x - i < MinX)
				{
					break;
				}

				var newPos = new Position(inPos.x - i, inPos.y);
				if(this[newPos].Block)
				{
					//このセルのみ影響、以降は影響しない
					yield return newPos;
					break;
				}

				yield return newPos;
			}

			for (var i = 1; i <= power; i++)
			{
				if (inPos.x + i > MinX + SizeX - 1)
				{
					break;
				}

				var newPos = new Position(inPos.x + i, inPos.y);
				if (this[newPos].Block)
				{
					//このセルのみ影響、以降は影響しない
					yield return newPos;
					break;
				}

				yield return newPos;
			}

			for (var i = 1; i <= power; i++)
			{
				if (inPos.y - i < MinY)
				{
					break;
				}

				var newPos = new Position(inPos.x, inPos.y - i);
				if (this[newPos].Block)
				{
					//このセルのみ影響、以降は影響しない
					yield return newPos;
					break;
				}

				yield return newPos;
			}

			for (var i = 1; i <= power; i++)
			{
				if (inPos.y + i > MinY + SizeY - 1)
				{
					break;
				}
				var newPos = new Position(inPos.x, inPos.y + i);
				if (this[newPos].Block)
				{
					//このセルのみ影響、以降は影響しない
					yield return newPos;
					break;
				}

				yield return newPos;
			}
		}


	}
}
