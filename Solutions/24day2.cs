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
			List<Report> reports = new List<Report>();

			StringUtils sUtil = new StringUtils();
			foreach(string line in input){
				string[] data = sUtil.SplitByChar(line, ' ');
				reports.Add(new Report(data, true));
			}
			
			return reports.Where(r => r.safe).Count().ToString();
		}


		public class Report{
			public List<int> levels;
			public bool safe;

			public Report(string[] input, bool partTwo = false){
				this.levels = new List<int>();
				foreach(string s in input){
					this.levels.Add(Int32.Parse(s));
				}
				if(partTwo){
					this.safe = this.IsSafePartTwo();
				}else{
					this.safe = this.IsSafe(this.levels);
				}
			}

			public bool IsSafe(List<int> items){
				bool safe = true;
				bool? decreasing = null;

				for(int iter = 0; iter < items.Count - 1; iter += 1){
					int currentChange = items[iter] - items[iter + 1];

					if(decreasing == null){
						if(currentChange > 0){
							decreasing = true; //current change numbers will be positive for a safe value
						}else{
							decreasing = false; //current change numbers will be negative for a safe value
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

				return safe;
			}

			public bool IsSafePartTwo(){
				if(!this.IsSafe(this.levels)){
					
					bool isSafe = false;
					//brute forcing all variations of each failed index because I'm lazy and I was definitely missing a case with the more clever solution I was trying to use
					for(int i = 0; i < this.levels.Count; i += 1){
						List<int> temp = new List<int>(this.levels);
						temp.RemoveAt(i);
						if(this.IsSafe(temp)){
							isSafe = true;
							break;
						}
					}

					return isSafe;
				}else{
					return true;
				}
			}
		}
	}
}