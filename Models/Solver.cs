namespace AoC24.Models{
	public interface Solver{
		public string Solve(List<string> input, int part = 1);
		public string PartOne(List<string> input);
		public string PartTwo(List<string> input);
	}
}