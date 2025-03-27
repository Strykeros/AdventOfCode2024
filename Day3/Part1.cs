using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day3
{
    internal class Part1
    {
        public void Run()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\input.txt");
            string fileTxt = File.ReadAllText(filePath);

            // Regex: mul(1-3 digit number,1-3 digit number)
            Regex regex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");

            MatchCollection matches = regex.Matches(fileTxt);

            int sum = 0;

            foreach (Match match in matches)
            {
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                int result = x * y;
                sum += result;
            }

            Console.WriteLine($"Sum of all valid mul operations: {sum}");
        }
    }
}
