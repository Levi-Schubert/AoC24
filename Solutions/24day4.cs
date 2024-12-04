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
			
			int total = 0;

			int m = puzzle.Count;
			int n = puzzle[0].Count;

			for (int i = 0; i < m; i += 1){
				for (int j = 0; j < n; j += 1){
					if(puzzle[i][j] == 'X'){
						int finds = Dfs(puzzle, i, j);
						total += finds;
					}
				}
			}

			return $"{total}";
		}

		public string PartTwo(List<string> input){
			List<List<char>> puzzle = Get2dCharArray(input);
			
			int total = 0;

			int m = puzzle.Count;
			int n = puzzle[0].Count;

			for (int i = 0; i < m; i += 1){
				for (int j = 0; j < n; j += 1){
					if(puzzle[i][j] == 'A'){
						if(CheckForXMAS(puzzle, i, j)){
							total += 1;
						}
					}
				}
			}

			return $"{total}";
		}

		private bool CheckForXMAS(List<List<char>> puzzle, int i, int j){
			//bounds check
			if (i - 1 < 0 || i + 1 >= puzzle.Count || j - 1 < 0 || j + 1 >= puzzle[0].Count) {
				return false;
			}
			char nw = puzzle[i-1][j-1];
			char ne = puzzle[i+1][j-1];
			char sw = puzzle[i-1][j+1];
			char se = puzzle[i+1][j+1];

			bool isXMAS = false;
			if ((nw == 'M' && se == 'S' && ne == 'M' && sw == 'S')
				|| (nw == 'S' && se == 'M' && ne == 'S' && sw == 'M')
				|| (nw == 'S' && se == 'M' && ne == 'M' && sw == 'S')
				|| (nw == 'M' && se == 'S' && ne == 'S' && sw == 'M')){
				isXMAS = true;
			}

			return isXMAS;
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