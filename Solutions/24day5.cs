using System.Collections.Generic;
using AoC24.Utils;
using AoC24.Models;

namespace AoC24.Solutions{
	public class TwentyFourDayFive : Solver{

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
			int total = 0;

			(List<PageOrder> pageOrders, List<int[]> pageGroups) = ParseInput(input);

			List<int[]> validGroups = new List<int[]>();

			foreach (int[] page in pageGroups){
				IEnumerable<PageOrder> applicable = pageOrders.Where(order =>  Array.IndexOf(page, order.Before) > -1);
				bool valid = true;
			
				foreach(PageOrder order in applicable){
					int aIndex = Array.IndexOf(page, order.After);
					int bIndex = Array.IndexOf(page, order.Before);
					if ((bIndex > aIndex && aIndex != -1)){
						valid = false;
						break;
					}
				}
				if(valid){
					validGroups.Add(page);
				}
			}

			foreach(int[] group in validGroups){
				int middleIndex = group.Length/2;
				total += group[middleIndex];
			}

			return $"{total}";
		}

		public string PartTwo(List<string> input){
			int total = 0;

			return $"{total}";
		}

		private (List<PageOrder>, List<int[]>) ParseInput(List<string> input){

			List<PageOrder> pageOrders = new List<PageOrder>();
			List<int[]> pageGroups = new List<int[]>();


			StringUtils sUtil = new StringUtils();
			bool firstDone = false;
			foreach (string line in input){
				if(line.Contains(',')){
					firstDone = true;
				}
				if(!firstDone){
					string[] spl = sUtil.SplitByChar(line, '|');
					PageOrder p = new PageOrder(Int32.Parse(spl[1]), Int32.Parse(spl[0]));
					pageOrders.Add(p);
				}else{
					string[] spl = sUtil.SplitByChar(line, ',');
					int[] group = new int[spl.Length];
					for(int i = 0; i < spl.Length; i += 1){
						group[i] = Int32.Parse(spl[i]);
					}
					pageGroups.Add(group);
				}
			}

			return (pageOrders, pageGroups);
		}

		class PageOrder{
			public int After {get; set;}
			public int Before {get; set;}

			public PageOrder (int a, int b){
				this.After = a;
				this.Before = b;
			}
		}

	}
}