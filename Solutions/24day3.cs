using System.Collections.Generic;
using System.Text.RegularExpressions;
using AoC24.Utils;
using AoC24.Models;

namespace AoC24.Solutions{
	public class TwentyFourDayThree : Solver{

		protected string _pattern = "([m])([u])([l])([(])([0-9]+)([,])([0-9]+)([)])";
		protected char[] _unused = ['>', '#', 'a', '+', 'i', ';', 'y', '^', 'f', 'n', '[', '@', '~', 'c', '-', 's', '{', '*', 'h', 'p', ':', ']', '\'', 'e', '/', '?', '}', 'b', ',', '%', 'r', '<', '!', 'o', ' ', 'w', '&', 'd', '$', 't'];


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
			var reg = new Regex(_pattern, RegexOptions.IgnoreCase);

			int total = 0;
			input.ForEach(line => {
				string s = line.Trim(_unused);
				Match m = reg.Match(s);
				while(m.Success){
					total += ParseMultiply(m.Value);
					m = m.NextMatch();
				}
			});

			return total.ToString();
		}

		public string PartTwo(List<string> input){
			

			return "NOT IMPLEMENTED";
		}

		private int ParseMultiply(string s){
			string temp = s.Trim(['m','u','l','(',')']);
			string[] ints = temp.Split(',');
			int result = Int32.Parse(ints[0]) * Int32.Parse(ints[1]);
			return result;
		}
	}
}