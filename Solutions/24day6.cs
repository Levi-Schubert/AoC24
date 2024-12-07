using System.Collections.Generic;
using AoC24.Utils;
using AoC24.Models;

namespace AoC24.Solutions{
	public class TwentyFourDaySix : Solver{

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

			Lab lab = new Lab(input);

			while(lab.OutOfBound == false){
				lab.TryStepForward();
			}

			foreach(List<Cell> row in lab.Grid){
				total += row.Where(c => c.Accessed).Count();
			}


			return $"{total}";
		}

		public string PartTwo(List<string> input){
			int total = 0;
			


			return $"{total}";
		}

		public class Lab{
			public List<List<Cell>> Grid {get; set;}

			public int GuardX {get; set;}

			public int GuardY {get; set;}

			public Facing GuardFacing {get; set;}

			public bool OutOfBound {get; set;}

			public Lab(List<string> input){
				this.Grid = new List<List<Cell>>();

				int y = 0;
				foreach(string line in input){
					int x = 0;
					List<Cell> columnList = new List<Cell>();
					foreach(char c in line){
						Cell cell = new Cell(c, x, y);
						columnList.Add(cell);
						if(cell.Occupied){
							this.GuardX = cell.X;
							this.GuardY = cell.Y;
							this.GuardFacing = cell.Direction;
						}
						x += 1;
					}
					this.Grid.Add(columnList);
					y += 1;
				}
				// this should probably be validated, 
				// I'm trusting the input doesn't start in front of the edge
				this.OutOfBound = false; 
			}

			public bool TryStepForward(){
				bool stepped = false;
				int xNew = -1;
				int yNew = -1;
				switch(this.GuardFacing){
					case Facing.North:
						xNew = this.GuardX;
						yNew = this.GuardY - 1;
						if(this.InBounds(xNew, yNew)){
							if(!this.CellObstructed(xNew, yNew)){
								this.UpdateCell(this.GuardX, this.GuardY, false, true);
								this.UpdateCell(xNew, yNew, true, true);
								this.GuardX = xNew;
								this.GuardY = yNew;
								stepped = true;
							}else{
								this.GuardFacing = Facing.East;
							}
						}else{
							this.OutOfBound = true;
						}
						break;
					case Facing.South:
						xNew = this.GuardX;
						yNew = this.GuardY + 1;
						if(this.InBounds(xNew, yNew)){
							if(!this.CellObstructed(xNew, yNew)){
								this.UpdateCell(this.GuardX, this.GuardY, false, true);
								this.UpdateCell(xNew, yNew, true, true);
								this.GuardX = xNew;
								this.GuardY = yNew;
								stepped = true;
							}else{
								this.GuardFacing = Facing.West;
							}
						}else{
							this.OutOfBound = true;
						}
						break;
					case Facing.East:
						xNew = this.GuardX + 1;
						yNew = this.GuardY;
						if(this.InBounds(xNew, yNew)){
							if(!this.CellObstructed(xNew, yNew)){
								this.UpdateCell(this.GuardX, this.GuardY, false, true);
								this.UpdateCell(xNew, yNew, true, true);
								this.GuardX = xNew;
								this.GuardY = yNew;
								stepped = true;
							}else{
								this.GuardFacing = Facing.South;
							}
						}else{
							this.OutOfBound = true;							
						}
						break;
					case Facing.West:
						xNew = this.GuardX - 1;
						yNew = this.GuardY;
						if(this.InBounds(xNew, yNew)){
							if(!this.CellObstructed(xNew, yNew)){
								this.UpdateCell(this.GuardX, this.GuardY, false, true);
								this.UpdateCell(xNew, yNew, true, true);
								this.GuardX = xNew;
								this.GuardY = yNew;
								stepped = true;
							}else{
								this.GuardFacing = Facing.North;
							}
						}else{
							this.OutOfBound = true;
						}
						break;
					default:
						break;//this shouldn't happen
				}
				return stepped;
			}


			private void UpdateCell(int x, int y, bool occupied, bool accessed){
				this.Grid[y][x].Occupied = occupied;
				this.Grid[y][x].Accessed = accessed;
			}

			private bool CellObstructed(int x, int y){
				return this.Grid[y][x].Obstruction;
			}

			private bool InBounds(int x, int y){
				return (y < this.Grid.Count && x < this.Grid[y].Count && x > -1 && y > -1);
			}
		}

		public class Cell{
			public bool Occupied {get; set;}

			public Facing Direction {get; set;}

			public bool Obstruction {get; set;}

			public bool Accessed {get; set;}

			public int X {get; set;}

			public int Y {get; set;}

			public Cell(char c, int x, int y){
				this.X = x;
				this.Y = y;
				switch(c){
					case '^':
						this.Occupied = true;
						this.Obstruction = false;
						this.Accessed = true;
						this.Direction = Facing.North;
						break;
					case 'v':
						this.Occupied = true;
						this.Obstruction = false;
						this.Accessed = true;
						this.Direction = Facing.South;
						break;
					case '>':
						this.Occupied = true;
						this.Obstruction = false;
						this.Accessed = true;
						this.Direction = Facing.East;
						break;
					case '<':
						this.Occupied = true;
						this.Obstruction = false;
						this.Accessed = true;
						this.Direction = Facing.West;
						break;
					case '#':
						this.Occupied = false;
						this.Accessed = false;
						this.Obstruction = true;
						this.Direction = Facing.None;
						break;
					default:
						this.Occupied = false;
						this.Accessed = false;
						this.Obstruction = false;
						this.Direction = Facing.None;
						break;
				}
			}
		}

		public enum Facing{
			North,
			South,
			East,
			West,
			None
		}

	}
}