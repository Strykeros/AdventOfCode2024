using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal class Part1
    {
        public void Run()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\input.txt");
            string[] reportLines = File.ReadAllLines(filePath);

            int safeCount = 0;

            foreach (string line in reportLines)
            {
                List<int> levels = ParseLine(line);

                if (IsSafeReport(levels))
                {
                    safeCount++;
                }
            }

            Console.WriteLine($"Safe reports: {safeCount}");
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

        // Check if a report is safe
        private static bool IsSafeReport(List<int> levels)
        {
            if (levels.Count < 2)
                return true; // trivially safe

            bool? increasing = null;

            for (int i = 1; i < levels.Count; i++)
            {
                int diff = levels[i] - levels[i - 1];

                if (diff == 0 || Math.Abs(diff) > 3)
                    return false; // invalid difference

                if (increasing == null)
                {
                    increasing = diff > 0;
                }
                else
                {
                    if ((diff > 0 && increasing == false) ||
                        (diff < 0 && increasing == true))
                    {
                        return false; // switched direction
                    }
                }
            }

            return true;
        }
    
    }
}
