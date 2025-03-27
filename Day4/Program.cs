using System.IO;

namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======================= PART 1 ============================");

            Part1 part1 = new Part1();
            part1.Run();

            Console.WriteLine("======================= PART 2 ============================");

            Part2 part2 = new Part2();
            part2.Run();
        }
    }
}
