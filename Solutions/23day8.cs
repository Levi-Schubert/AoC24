using System.Collections.Generic;
using AoC24.Utils;

namespace AoC24.Solutions{
	public class TwentyThreeDayEight{
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
			char[] directions = input[0].ToCharArray();
			input.RemoveAt(0);
			input.RemoveAt(0); // remove whitespace line
			SortedDictionary<string, Direction> map = ParseInputOne(input);
			int steps = 0;

			Direction current = map["AAA"];

			while (current.CurrentLocation != "ZZZ"){
				switch(directions[steps % directions.Length]){
					case 'L':
						current = map[current.Left];
						break;
					case 'R':
						current = map[current.Right];
						break;
					default: 
						break;
				}
				steps += 1;
			}

			return steps.ToString();
		}

		public string PartTwo(List<String> input){
			string answer = input.Count.ToString();

			return answer;
		}

		SortedDictionary<string, Direction> ParseInputOne(List<string> input){
			SortedDictionary<string, Direction> map = new SortedDictionary<string, Direction>();

			StringUtils str = new StringUtils();

			foreach(string line in input){
				if(!String.IsNullOrWhiteSpace(line)){
					string[] temp = str.SplitByChar(line, '=');
					string current = str.RemoveChars(temp[0],' ');
					temp = str.SplitByChar(str.RemoveChars(temp[1], ['(',')', ' ']), ',');
					Direction dir = new Direction(current, temp[0], temp[1]);
					map.Add(current, dir);
				}
			}

			return map;
		}



		public class Direction{
			public string CurrentLocation {get; set;}
			public string Left {get; set;}
			public string Right {get; set;}

			public Direction(string cur, string left, string right){
				this.CurrentLocation = cur;
				this.Left = left;
				this.Right = right;
			}
		}
	}
}