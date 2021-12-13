using Black_hole.Persistence;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Black_hole.Model
{
	public enum Direction { Vertical = 0, Horizontal = 1 }
	public class BlackHoleModel
	{
		
		public int size { get; private set; }
		public int[,] table { get; private set; }
		public int[] scores { get; private set; }
		public int currentPlayer { get; private set; }
		public int winner { get; private set; }

		private BlackHoleDataAccess dataAccess;
		public BlackHoleModel()
		{
			newGame(5); // basic size
			dataAccess = new BlackHoleDataAccess();
		}
		public void newGame(int size)
		{
			currentPlayer = 1;
			scores = new int[2];
			scores[0] = 0;
			scores[1] = 0;
			this.size = size;
			this.table = new int[size, size];
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if (i == ((size) / 2))
					{
						table[i, j] = i == j ? -1 : 0;
					}
					else if (i < (size ) / 2)
					{
						table[i, j] = (i == j) || (i+j == size-1) ? 1 : 0;
					}
					else
					{
						table[i, j] = (i == j) || (i + j == size - 1) ? 2 : 0;
					}
				}
			}
		}
		public event EventHandler<int> GameOver;
		public event EventHandler<int> ChangeLabel;
		public event EventHandler RefreshTable;
		public void Step(int i, int j, int way, Direction dir)
		{
			int player = table[i, j];
			if (player == 0 || player == -1) { return; }
			switch (dir)
			{
				case Direction.Vertical: //fuggoleges
					int newDir = i + way;
					while (newDir >= 0 && newDir < size && table[newDir, j] == 0)
					{
						table[i, j] = 0;
						table[newDir, j] = player;
						i = newDir;
						newDir += way;
					}
					if (newDir >= 0 && newDir < size && table[newDir, j] == -1)
					{
						table[i, j] = 0;
						scores[player - 1]++;
					}
					break;
				case Direction.Horizontal://vizszintes
					int newDirJ = j + way;
					while (newDirJ >= 0 && newDirJ < size && table[i, newDirJ] == 0)
					{
						table[i, j] = 0;
						table[i, newDirJ] = player;
						j = newDirJ;
						newDirJ += way;
					}
					if (newDirJ >= 0 && newDirJ < size && table[i, newDirJ] == -1)
					{
						table[i, j] = 0;
						scores[player - 1]++;
					}
					break;
			}
			if(scores[player-1] >= size / 2)
            {
				winner = player;
				if(GameOver != null)
				GameOver(this, winner);
			}
			currentPlayer = currentPlayer == 1 ? 2 : 1;
			if(ChangeLabel != null)
			ChangeLabel(this, currentPlayer);
			if(RefreshTable != null)
            {
				RefreshTable(this, EventArgs.Empty);
            }
			
		}
		public async Task LoadGameAsync(String path)
		{
			if (dataAccess == null)
				throw new InvalidOperationException("No data access is provided.");

			BlackHoleTable bht = await dataAccess.LoadAsync(path);
			this.currentPlayer = bht.currentPlayer;
			this.size = bht.Size;
			this.scores = bht.scores;
			this.table = bht.table;
			ChangeLabel(this, currentPlayer);
		}
		
		public async Task SaveGameAsync(string path)
		{
			if (dataAccess == null)
				throw new InvalidOperationException("No data access is provided.");

			await dataAccess.SaveAsync(path, new BlackHoleTable(currentPlayer,scores,table));
		}
	}

}