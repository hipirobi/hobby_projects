using System;

namespace Black_hole.Persistence
{

	public class BlackHoleTable
	{
		public int currentPlayer { get; private set; }
		public int[] scores { get; private set; }
		public int[,] table { get; private set; }
		public BlackHoleTable(int currentPlayer, int[] scores, int[,] table)
		{
			this.currentPlayer = currentPlayer;
			this.scores = scores;
			this.table = table;
		}
		public int Size{ get { return table.GetLength(0); } }
	}
}
