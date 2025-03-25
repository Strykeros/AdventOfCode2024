using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    internal class Part2
    {
        public void Run()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\input.txt");
            string[] lineRows = File.ReadAllLines(filePath);

            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();

            foreach (string line in lineRows)
            {
                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int num1) &&
                    int.TryParse(parts[1], out int num2))
                {
                    leftList.Add(num1);
                    rightList.Add(num2);
                }
            }

            Dictionary<int, int> rightCounts = new Dictionary<int, int>();
            foreach (int number in rightList)
            {
                if (rightCounts.ContainsKey(number))
                    rightCounts[number]++;
                else
                    rightCounts[number] = 1;
            }

            int similarityScore = 0;
            foreach (int number in leftList)
            {
                if (rightCounts.TryGetValue(number, out int count))
                {
                    similarityScore += number * count;
                }
            }

            Console.WriteLine($"Similarity score: {similarityScore}");
        }
    }
}
