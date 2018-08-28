using CSBombmanClientNak.ModelInternal;
using CSBombmanServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSBombmanClientNak
{
	enum ThreatLevel { None, Danger, Death };
	enum Direction { North, South, West, East, None };

	class Threat
	{
		public ThreatLevel ThreatLevel { get; set; }
		public Direction Direction { get; set; }
	}


	public class Program
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		static void WaitForDebuggerAttach()
		{
			//	Console.WriteLine("Waiting for debugger to attach");
			while (!Debugger.IsAttached)
			{
				Thread.Sleep(100);
			}
			//	Console.WriteLine("Debugger attached");
		}


		public static (string, int) Example()
		{
			return ("aaa", 1);
		}

		static void Main(string[] args)
		{

			try
			{

				Console.InputEncoding = Encoding.UTF8;
				Console.OutputEncoding = Encoding.UTF8;
				Console.WriteLine(Consts.MyName, Console.OutputEncoding.CodePage);
				//標準入力
				//WaitForDebuggerAttach();

				var sPos = Console.ReadLine();
				int position = Convert.ToInt32(sPos);

				var moveDecider = new ActionDecider();

				while (true)
				{
					logger.Debug("**************************");

					var s = Console.ReadLine();

					Stopwatch stopWatch = new Stopwatch();
					stopWatch.Start();

					logger.Debug("--- input ----------------");
					if(s == null)
					{
						logger.Debug("null");
					}
					else
					{
						logger.Debug($"length {s.Length}");
						logger.Debug(s);
					}
					logger.Factory.Flush();
					logger.Debug("--------------------------");

					var map = Utils.JsonToObject<MapData>(s);

					var internalMap = new InternalMapData(map);
					Action m = moveDecider.NextMove(internalMap);

					stopWatch.Stop();
					TimeSpan ts = stopWatch.Elapsed;

					Console.WriteLine(m.ToCommandString());
					logger.Debug(m.ToCommandString());
					logger.Debug(ts.ToString());
				}
			}
			catch (Exception e)
			{
				logger.Debug(e);
				logger.Factory.Flush();
				Console.WriteLine(e);
			}
		}


	}
}
