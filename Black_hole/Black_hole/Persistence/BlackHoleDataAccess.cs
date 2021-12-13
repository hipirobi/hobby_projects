using System;
using System.IO;
using System.Threading.Tasks;

namespace Black_hole.Persistence
{

	public class BlackHoleDataAccess
	{
		public BlackHoleTable table { get; set; } 
		public BlackHoleDataAccess()
		{
		}

		public async Task<BlackHoleTable> LoadAsync(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path)) // fájl megnyitása
                {
                    int currentPlayer;
                    int[] scores = new int[2];
                    int[,] table;
                    string line = await reader.ReadLineAsync();
                    string[] numbers = line.Split(" ");
                    currentPlayer = Int32.Parse(numbers[0]);
                    scores[0] = Int32.Parse(numbers[1]);
                    scores[1] = Int32.Parse(numbers[2]);
                    int size = Int32.Parse(numbers[3]);
                    table = new int[size, size];
                    for (Int32 i = 0; i < size; i++)
                    {
                        line = await reader.ReadLineAsync();
                        numbers = line.Split(' ');

                        for (Int32 j = 0; j < size; j++)
                        {
                            table[i,j] = Int32.Parse(numbers[j]);
                        }
                    }

                    return new BlackHoleTable(currentPlayer,scores,table);
                }
            }
            catch
            {
                throw new Exception("File megnyitas kivétel keletkezett.");
            }
        }

		public async Task SaveAsync(String path, BlackHoleTable table)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása
                {
                    writer.Write(table.currentPlayer + " " + table.scores[0] + " " + table.scores[1] + " "+ table.Size+" \n");
                    for (Int32 i = 0; i < table.Size; i++)
                    {
                        for (Int32 j = 0; j < table.Size; j++)
                        {
                            await writer.WriteAsync(table.table[i, j] + " "); // kiírjuk az értékeket
                        }
                        await writer.WriteAsync("\n");
                        
                    }
                }
            }
            catch
            {
                throw new Exception("File mentes kivétel keletkezett.");
            }
        }
	}

}