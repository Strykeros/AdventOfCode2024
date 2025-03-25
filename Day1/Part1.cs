using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    internal class Part1
    {
        public void Run()
        {
            // get full file path
            string filePath = Directory.GetCurrentDirectory() + "C:\\..\\..\\..\\input.txt";
            string[] lineRows = File.ReadAllLines(filePath);

            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            int totalDistance = 0;

            for (int i = 0; i < lineRows.Length; i++)
            {
                string[] txtNumbers = lineRows[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (txtNumbers.Length == 2 &&
                    int.TryParse(txtNumbers[0], out int num1) &&
                    int.TryParse(txtNumbers[1], out int num2))
                {
                    list.Add(num1);
                    list2.Add(num2);
                }
            }

            list = SortList(list);
            list2 = SortList(list2);

            for (int i = 0; i < list.Count; i++)
            {
                totalDistance += Math.Abs(list[i] - list2[i]);
            }

            Console.WriteLine(totalDistance);
        }

        private List<int> SortList(List<int> list)
        {
            List<int> sortedList = new List<int>(list);

            // Selection sort
            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < sortedList.Count; j++)
                {
                    if (sortedList[j] < sortedList[minIndex])
                    {
                        minIndex = j;
                    }
                }

                int temp = sortedList[i];
                sortedList[i] = sortedList[minIndex];
                sortedList[minIndex] = temp;
            }

            return sortedList;
        }
    }
}
