

namespace AoC24.Utils{
	
	public class StringUtils{

		public string[] SplitByChar(string s, char c){
			return s.Split(c);
		}

		public string ReplaceChars(string s, char c, char replace){
			s = s.Replace(c, replace);
			return s;
		}

		public string RemoveChars(string s, char[] chars){
			foreach (char c in chars){
				s = RemoveChars(s, c);
			}
			return s;
		}

		public string RemoveChars(string s, char c){
			s = s.Replace(c.ToString(), String.Empty);
			return s;
		}

		public string ReplaceChars(string s, char[] chars, char replace){
			foreach (char c in chars){
				s = ReplaceChars(s, c, replace);
			}
			return s;
		}
	}
}