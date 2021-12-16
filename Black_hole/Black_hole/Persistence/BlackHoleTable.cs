using System;

namespace Black_hole.Persistence
{

	public class BlackHoleTable
	{
		public int CurrentPlayer { get; private set; }
		public int[] Scores { get; private set; }
		public int[,] Table { get; private set; }
		public BlackHoleTable(int currentPlayer, int[] scores, int[,] table)
		{
			this.CurrentPlayer = currentPlayer;
			this.Scores = scores;
			this.Table = table;
		}
		public int Size{ get { return Table.GetLength(0); } }
	}
}
