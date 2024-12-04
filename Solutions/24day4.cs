using System.Collections.Generic;
using AoC24.Utils;
using AoC24.Models;

namespace AoC24.Solutions{
	public class TwentyFourDayFour : Solver{

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
			List<List<char>> puzzle = Get2dCharArray(input);
			
			int count = CheckForMatches(puzzle);

			return $"{count}";
		}

		public string PartTwo(List<string> input){
			return "NOT IMPLEMENTED";
		}


		private int CheckForMatches(List<List<char>> puzzle){
			int total = 0;

			int m = puzzle.Count;
			int n = puzzle[0].Count;

			for (int i = 0; i < m; i += 1){
				int rowcount = 0;
				for (int j = 0; j < n; j += 1){
					if(puzzle[i][j] == 'X'){
						int finds = Dfs(puzzle, i, j);
						total += finds;
						rowcount += finds;
							//Console.WriteLine($"match found start index [{i},{j}]");
					}
				}
			}


			return total;
		}

		private int Dfs(List<List<char>> puzzle, int i, int j){

			List<bool> matches = new List<bool>();

			matches.Add(Dfs(puzzle, i -1, j -1, 1, "NW"));
			matches.Add(Dfs(puzzle, i -1, j, 1, "W"));
			matches.Add(Dfs(puzzle, i - 1, j + 1, 1, "SW"));
			matches.Add(Dfs(puzzle, i + 1, j - 1, 1, "NE"));
			matches.Add(Dfs(puzzle, i + 1, j, 1, "E"));
			matches.Add(Dfs(puzzle, i + 1, j + 1, 1, "SE"));
			matches.Add(Dfs(puzzle, i, j - 1, 1, "N"));
			matches.Add(Dfs(puzzle, i, j + 1, 1, "S"));

			return matches.Where(m => m).Count();
		}


		private bool Dfs(List<List<char>> puzzle, int i, int j, int index, string direction = ""){
			string word = "XMAS";
			if (index == word.Length) {
				return true;
			}
			if (i < 0 || i >= puzzle.Count || j < 0 || j >= puzzle[0].Count || puzzle[i][j] != word[index]) {
				return false;
			}

			char temp = puzzle[i][j];
			puzzle[i][j] = '#';  // mark as visited

			bool found = false;
			switch (direction){
				case "NW":
					found = Dfs(puzzle, i -1, j -1, index + 1, "NW");
					break;
				case "W":
					found = Dfs(puzzle, i -1, j, index + 1, "W");
					break;
				case "SW":
					found = Dfs(puzzle, i - 1, j + 1, index + 1, "SW");
					break;
				case "NE":
					found = Dfs(puzzle, i + 1, j - 1, index + 1, "NE");
					break;
				case "E":
					found = Dfs(puzzle, i + 1, j, index + 1, "E");
					break;
				case "SE":
					found = Dfs(puzzle, i + 1, j + 1, index + 1, "SE");
					break;
				case "N":
					found = Dfs(puzzle, i, j - 1, index + 1, "N");
					break;
				case "S":
					found = Dfs(puzzle, i, j + 1, index + 1, "S");
					break;
				default:
					found = false;
					break;
			}
			

			puzzle[i][j] = temp;  // unmark

			return found;
		}

		


		private List<List<char>> Get2dCharArray(List<string> input){
			List<List<char>> puzzle = new List<List<char>>();
			
			foreach(string line in input){
				List<char> row = new List<char>();
				foreach(char c in line){
					row.Add(c);
				}
				puzzle.Add(row);
			}
			return puzzle;
		}

	}
}