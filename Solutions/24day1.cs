using System.Collections.Generic;
using AoC24.Utils;
using AoC24.Models;

namespace AoC24.Solutions{
	public class TwentyFourDayOne : Solver{
		public string Solve(List<string> input, int part = 1){
			string answer = "";

			if(part ==  1){
				answer = PartOne(input);
			}else{
				answer = PartTwo(input);
			}

			return answer;
		}

		public string PartOne(List<string> input){
			int distance = 0;

			List<string> left = new List<string>();
			List<string> right = new List<string>();

			StringUtils s = new StringUtils();
			
			foreach (string line in input){
				//note modified input file from three spaces between each value to a single tab
				string[] t = s.SplitByChar(line, '	');
				left.Add(t[0]);
				right.Add(t[1]);
			}
			
			left.Sort();
			right.Sort();

			while(left.Count > 0){
				int moved = Int32.Parse(left[0]) - Int32.Parse(right[0]);
				if(moved < 0){moved *= -1;}
				distance += moved;
				
				left.RemoveAt(0);
				right.RemoveAt(0);
			}

			return distance.ToString();
		}

		public string PartTwo(List<string> input){
			int distance = 0;

			List<string> left = new List<string>();
			List<string> right = new List<string>();

			StringUtils s = new StringUtils();
			
			foreach (string line in input){
				//note modified input file from three spaces between each value to a single tab
				string[] t = s.SplitByChar(line, '	');
				left.Add(t[0]);
				right.Add(t[1]);
			}

			int total = 0;
			foreach(string item in left){
				var matches = right.FindAll(t => t == item);
				if(matches.Count > 0){
					total += (Int32.Parse(item) * matches.Count);
				}
			}

			return total.ToString();
		}
	}
}