using System.Collections.Generic;
using AoC24.Utils;
using AoC24.Models;

namespace AoC24.Solutions{
	public class TwentyFourDayTwo : Solver{
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

			List<Report> reports = new List<Report>();

			StringUtils sUtil = new StringUtils();
			foreach(string line in input){
				string[] data = sUtil.SplitByChar(line, ' ');
				reports.Add(new Report(data));
			}
			
			return reports.Where(r => r.safe).Count().ToString();
		}

		public string PartTwo(List<string> input){
			int distance = 0;

			
			return distance.ToString();
		}


		public class Report{
			public List<int> levels;
			public bool safe;

			public Report(string[] input){
				this.levels = new List<int>();
				foreach(string s in input){
					this.levels.Add(Int32.Parse(s));
				}
				this.safe = this.IsSafe();
			}

			public bool IsSafe(){
				bool safe = true;

				bool? decreasing = null;

				int largestDiff = 0;
				int smallestDiff = 0;

				for(int iter = 0; iter < this.levels.Count - 1; iter += 1){
					int currentChange = this.levels[iter] - this.levels[iter + 1];
					//if valid change amount
					if(currentChange > largestDiff){largestDiff = currentChange;}
					if(currentChange < smallestDiff){smallestDiff = currentChange;}
				
					if(decreasing == null){
						if(currentChange > 0){
							decreasing = true;
						}else{
							decreasing = false;
						}
					}
					if(decreasing == false && (currentChange < -3 || currentChange >= 0)){
						safe = false;
						break;
					}
					if(decreasing == true && (currentChange > 3 || currentChange <= 0)){
						safe = false;
						break;
					}
				}

				if((smallestDiff < -3 || largestDiff > 3) && safe == true){
					if(decreasing == true){
						Console.WriteLine($"is safe: {safe} | decreasing: {decreasing} | highest: {largestDiff} [{String.Join(", ", this.levels.ToArray())}]");
					}else{
						Console.WriteLine($"is safe: {safe} | decreasing: {decreasing} | lowest: {smallestDiff} [{String.Join(", ", this.levels.ToArray())}]");
					}
				}

				return safe;
			}
		}
	}
}