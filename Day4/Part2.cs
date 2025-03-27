using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    internal class Part2
    {
        public void Run()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\input.txt");
            string[] lines = File.ReadAllLines(filePath);
            char[][] grid = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
                grid[i] = lines[i].ToCharArray();

            int rowCount = grid.Length;
            int colCount = grid[0].Length;
            int xMasCount = 0;

            for (int r = 1; r < rowCount - 1; r++)
            {
                for (int c = 1; c < colCount - 1; c++)
                {
                    if (grid[r][c] != 'A')
                        continue;

                    // Diagonal ↘ (top-left to bottom-right)
                    string diag1 = $"{grid[r - 1][c - 1]}{grid[r][c]}{grid[r + 1][c + 1]}";
                    // Diagonal ↙ (top-right to bottom-left)
                    string diag2 = $"{grid[r - 1][c + 1]}{grid[r][c]}{grid[r + 1][c - 1]}";

                    if (IsMASPattern(diag1) && IsMASPattern(diag2))
                        xMasCount++;
                }
            }

            Console.WriteLine($"Total X-MAS patterns: {xMasCount}");
        }

        private bool IsMASPattern(string s)
        {
            return s == "MAS" || s == "SAM";
        }
    }
}
