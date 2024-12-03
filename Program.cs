using System;

using System.Diagnostics;
using System.Threading;

using AoC24.Utils;
using AoC24.Solutions;
using AoC24.Models;

namespace AoC24{

	internal class Program{

		static void Main(string[] args){
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			int year = Int32.Parse(args[0]);
			int day = Int32.Parse(args[1]);
			int part = Int32.Parse(args[2]);

			string answer = GetSolution(year, day, part);

			stopWatch.Stop();
			TimeSpan ts = stopWatch.Elapsed;
			String timeTaken = ts.ToString("G");
			Console.WriteLine($"answer: {answer} \nobtained in: {timeTaken}");
		}

		static string GetFileName(int year, int day){
			return $"./inputs/{year}-{day}.txt";
		}

		static string GetSolution(int year, int day, int part){
			string answer;
			string filename = GetFileName(year, day);

			switch (year){
				case 24:
					answer = Year24(day, part, filename);
					break;
				case 23:
					answer = Year23(day, part, filename);
					break;
				default:
					answer = "invalid year";
					break;
			}

			return answer;
		}

		static string Year23(int day, int part, string filename){
			string answer;

			FileUtils files = new FileUtils();
			List<string> input = files.ReadLinesFromFile(filename);

			Solver? solver = null;

			switch (day){
				case 8:
					solver = new TwentyThreeDayEight();
					break;
				default:
					answer = "invalid day";
					break;
			}

			if(solver != null){
				if(part == 1){
					answer = solver.Solve(input);
				}else{
					answer = solver.Solve(input, 2);
				}
			}else{
				answer = "no solver found";
			}

			return answer;
		}

		static string Year24(int day, int part, string filename){
			string answer;

			FileUtils files = new FileUtils();
			List<string> input = files.ReadLinesFromFile(filename);

			Solver? solver = null;

			switch (day){
				case 1:
					solver = new TwentyFourDayOne();
					break;
				case 2:
					solver = new TwentyFourDayTwo();
					break;
				case 3:
					solver = new TwentyFourDayThree();
					break;
				default:
					answer = "invalid day";
					break;
			}

			if(solver != null){
				if(part == 1){
					answer = solver.Solve(input);
				}else{
					answer = solver.Solve(input, 2);
				}
			}else{
				answer = "no solver found";
			}

			return answer;
		}
	}
}

