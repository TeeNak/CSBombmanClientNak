using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSBombmanClientNak.ModelInternal;
using CSBombmanServer;

namespace CSBombmanClientNak
{

	enum ThreatLevel { None, Danger, Death };
	enum Direction { North, South, West, East, None };

	class Threat
	{
		public ThreatLevel ThreatLevel {get; set;}
		public Direction Direction { get; set; }
	}


	class Program
	{
		static void WaitForDebuggerAttach()
		{
		//	Console.WriteLine("Waiting for debugger to attach");
			while (!Debugger.IsAttached)
			{
				Thread.Sleep(100);
			}
		//	Console.WriteLine("Debugger attached");
		}

		static void Main(string[] args)
		{

			using (var logFile = new StreamWriter(@".\log.txt"))
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
						var s = Console.ReadLine();

						var map = Utils.JsonToObject<MapData>(s);

						var internalMap = new InternalMapData(map); 
						Action m = moveDecider.NextMove(internalMap);

						Console.WriteLine(m.ToCommandString());
						logFile.WriteLine(m.ToCommandString());
						logFile.Flush();
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					logFile.WriteLine(e);
					logFile.Flush();
				}
			}
		}


	}
}
