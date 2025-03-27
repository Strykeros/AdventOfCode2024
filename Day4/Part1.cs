using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day4
{
    internal class Part1
    {
        public void Run()
        {
            List<(int dx, int dy)> directions = new List<(int, int)>
{
    (0, 1),    // right →
    (0, -1),   // left ←
    (1, 0),    // down ↓
    (-1, 0),   // up ↑
    (1, 1),    // down-right ↘
    (1, -1),   // down-left ↙
    (-1, 1),   // up-right ↗
    (-1, -1)   // up-left ↖
};
            // get full file path
            string filePath = Directory.GetCurrentDirectory() + "C:\\..\\..\\..\\input.txt";

            string[] lines = File.ReadAllLines(filePath);
            char[][] grid = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
                grid[i] = lines[i].ToCharArray();

            int totalCount = 0;

            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    foreach (var (dx, dy) in directions)
                    {
                        if (MatchesXMAS(grid, row, col, dx, dy))
                            totalCount++;
                    }
                }
            }

            Console.WriteLine($"Total 'XMAS' occurrences: {totalCount}");
        }

        private bool MatchesXMAS(char[][] grid, int row, int col, int dx, int dy)
        {
            string targetName = "XMAS";
            for (int k = 0; k < targetName.Length; k++)
            {
                int newRow = row + dx * k;
                int newCol = col + dy * k;

                if (newRow < 0 || newRow >= grid.Length || newCol < 0 || newCol >= grid[0].Length)
                    return false;

                if (grid[newRow][newCol] != targetName[k])
                    return false;
            }

            return true;
        }
    }
}
