using System.IO;


namespace AoC24.Utils{
	
	public class FileUtils{

		public List<string> ReadLinesFromFile(string filename){
			List<string> lines = new List<string>();

			foreach(string line in File.ReadLines(filename)){
				if(!String.IsNullOrWhiteSpace(line)){
					lines.Add(line);
				}
			}

			return lines;
		}
	}
}