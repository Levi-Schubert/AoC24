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

			(List<PageOrder> pageOrders, List<int[]> validGroups, List<(IEnumerable<PageOrder>, int[])> invalidGroups) = GetPageOrdersAndLists(input);

			foreach(int[] group in validGroups){
				int middleIndex = group.Length/2;
				total += group[middleIndex];
			}

			return $"{total}";
		}

		public string PartTwo(List<string> input){
			int total = 0;
			(List<PageOrder> pageOrders, List<int[]> validGroups, List<(IEnumerable<PageOrder>, int[])> invalidGroups) = GetPageOrdersAndLists(input);

			List<int[]> correctedList = new List<int[]>();

			//.Item1 .Item2
			foreach(var group in invalidGroups){
				int[] corrected = CorrectGroup(group.Item1, group.Item2);
				if(corrected != null){
					correctedList.Add(corrected);
				}
			}

			foreach(int[] group in correctedList){
				int middleIndex = group.Length/2;
				total += group[middleIndex];
			}

			return $"{total}";
		}

		private int[] CorrectGroup(IEnumerable<PageOrder> applicable, int[] page){
			int[] retval = null;
			List<int> tempPage = new List<int>(page);

			// IEnumerable<PageOrder> sortedApplicable = applicable.Sort()

			foreach(PageOrder order in applicable){
				(int aIndex, int bIndex) = GetOrderIndexes(page, order);
				if(aIndex != -1 && bIndex > aIndex){
					int item = tempPage[bIndex];
					tempPage.RemoveAt(bIndex);
					tempPage.Insert(aIndex, item);
					Console.WriteLine($"correcting [{String.Join(',', tempPage.ToArray())}], Order: {order.Before} before {order.After}");
				}
				if(IsPageValid(applicable, tempPage.ToArray())){
					retval = tempPage.ToArray();
				}
			}

			return retval;
		}

		private (List<PageOrder>, List<int[]>, List<(IEnumerable<PageOrder>, int[])>) GetPageOrdersAndLists(List<string> input){
			(List<PageOrder> pageOrders, List<int[]> pageGroups) = ParseInput(input);

			List<int[]> validGroups = new List<int[]>();
			List<(IEnumerable<PageOrder>, int[])> invalidGroups = new List<(IEnumerable<PageOrder>, int[])>();

			foreach (int[] page in pageGroups){
				IEnumerable<PageOrder> applicable = pageOrders.Where(order =>  Array.IndexOf(page, order.Before) > -1 && Array.IndexOf(page, order.After) > -1);
				bool valid = IsPageValid(applicable, page);
				if(valid){
					validGroups.Add(page);
				}else{
					invalidGroups.Add((applicable,page));
				}
			}

			return (pageOrders, validGroups, invalidGroups);
		}

		private bool IsPageValid(IEnumerable<PageOrder> applicable, int[] page){
			bool valid = true;
			foreach(PageOrder order in applicable){
					(int aIndex, int bIndex) = GetOrderIndexes(page, order);
					if ((bIndex > aIndex && aIndex != -1)){
						valid = false;
						break;
					}
			}
			return valid;
		}

		private (int, int) GetOrderIndexes(int[] page, PageOrder order){
			int aIndex = Array.IndexOf(page, order.After);
			int bIndex = Array.IndexOf(page, order.Before);
			return (aIndex, bIndex);
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