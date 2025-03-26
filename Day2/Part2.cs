using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal class Part2
    {
        public void Run()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\input.txt");
            string[] reportLines = File.ReadAllLines(filePath);

            int safeCount = 0;

            foreach (string line in reportLines)
            {
                List<int> levels = ParseLine(line);

                if (IsSafeReport(levels) || CanBeSafeWithOneRemoval(levels))
                {
                    safeCount++;
                }
            }

            Console.WriteLine($"Safe reports (with dampener): {safeCount}");

        }

        // Parse a line into a list of ints
        private static List<int> ParseLine(string line)
        {
            List<int> numbers = new List<int>();
            string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts)
            {
                if (int.TryParse(part, out int num))
                    numbers.Add(num);
            }

            return numbers;
        }

        // Check if a report is safe using the original rules
        private static bool IsSafeReport(List<int> levels)
        {
            if (levels.Count < 2)
                return true; // trivially safe

            bool? increasing = null;

            for (int i = 1; i < levels.Count; i++)
            {
                int diff = levels[i] - levels[i - 1];

                if (diff == 0 || Math.Abs(diff) > 3)
                    return false;

                if (increasing == null)
                {
                    increasing = diff > 0;
                }
                else if ((diff > 0 && increasing == false) || (diff < 0 && increasing == true))
                {
                    return false;
                }
            }

            return true;
        }

        // Check if the report can be safe by removing one level
        private static bool CanBeSafeWithOneRemoval(List<int> levels)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                List<int> modified = new List<int>(levels);
                modified.RemoveAt(i);

                if (IsSafeReport(modified))
                {
                    return true;
                }
            }

            return false;
        }
    }

}
