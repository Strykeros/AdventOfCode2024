using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day3
{
    internal class Part2
    {
        public void Run()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\input.txt");
            string memory = File.ReadAllText(filePath);

            int total = 0;
            bool mulEnabled = true;

            // Match valid mul(x,y), do(), or don't()
            Regex regex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)");

            MatchCollection matches = regex.Matches(memory);

            foreach (Match match in matches)
            {
                if (match.Value == "do()")
                {
                    mulEnabled = true;
                }
                else if (match.Value == "don't()")
                {
                    mulEnabled = false;
                }
                else if (match.Value.StartsWith("mul(") && mulEnabled)
                {
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);
                    total += x * y;
                }
            }

            Console.WriteLine($"Sum of enabled mul operations: {total}");
        }
    }
}
