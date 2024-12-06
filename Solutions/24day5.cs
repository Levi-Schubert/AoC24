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

			(List<int[]> validGroups, List<(IEnumerable<PageOrder>, int[])> invalidGroups) = GetPageOrdersAndLists(input);

			foreach(int[] group in validGroups){
				int middleIndex = group.Length/2;
				total += group[middleIndex];
			}

			return $"{total}";
		}

		public string PartTwo(List<string> input){
			int total = 0;
			(List<int[]> validGroups, List<(IEnumerable<PageOrder>, int[])> invalidGroups) = GetPageOrdersAndLists(input);

			List<int[]> correctedList = new List<int[]>();

			foreach(var group in invalidGroups){
				int[]? corrected = CorrectGroup(group.Item1, group.Item2);
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

		private int[]? CorrectGroup(IEnumerable<PageOrder> applicable, int[] page, int pass = 0){
			int[]? retval = null;
			List<int> tempPage = new List<int>(page);

			IEnumerable<PageOrder> correct = applicable.Where(po => po.PageOrderValid(tempPage.ToArray()));
			IEnumerable<PageOrder> incorrect = applicable.Where(po => !po.PageOrderValid(tempPage.ToArray()));

			foreach(PageOrder order in incorrect){
				bool ruleValid = order.PageOrderValid(tempPage.ToArray());
				if(!ruleValid){
					(int aIndex, int bIndex) = GetOrderIndexes(tempPage.ToArray(), order);
					int item = tempPage[bIndex];
					tempPage.RemoveAt(bIndex);
					tempPage.Insert(aIndex, item);
				}
			}
			correct = applicable.Where(po => po.PageOrderValid(tempPage.ToArray()));
			incorrect = applicable.Where(po => !po.PageOrderValid(tempPage.ToArray()));

			if(incorrect.Count() == 0){
				retval = tempPage.ToArray();
			}else{
				if(incorrect.Count() > 0 && pass < 1){
					retval = CorrectGroup(applicable, tempPage.ToArray(), pass + 1);
				}
			}

			return retval;
		}

		private (List<int[]>, List<(IEnumerable<PageOrder>, int[])>) GetPageOrdersAndLists(List<string> input){
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

			return (validGroups, invalidGroups);
		}

		private bool IsPageValid(IEnumerable<PageOrder> applicable, int[] page){
			bool valid = true;
			foreach(PageOrder order in applicable){
				valid = order.PageOrderValid(page);
				if(!valid){
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

			public bool PageOrderValid(int[] group){
				int aIndex = Array.IndexOf(group, this.After);
				int bIndex = Array.IndexOf(group, this.Before);
				return (aIndex < 0 || bIndex < aIndex);
			}
		}

	}
}