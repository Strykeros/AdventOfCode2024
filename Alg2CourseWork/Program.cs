namespace Alg2CourseWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\Juzer\\Desktop\\Programming\\C#\\Alg2CourseWork\\input.txt";

            string[] lines = File.ReadAllLines(filePath);
            int lineCount = lines.Length;
            int lineSymbolCount = lines[0].Length;

            char[,] fileCharArray = new char[lineCount, lineSymbolCount];
            List<(int, int)> startPoint = new List<(int, int)>();
            List<(int, int)> finishPoint = new List<(int, int)>();

            int[][] directions = { [0, +1], [0, -1], [-1, 0], [+1, 0] };
            int[][] directionsWithCheats = { [0, +2], [+2, 0], [0, -2], [-2, 0], [+1, +1], [-1, +1], [+1, -1], [-1, -1] };
            //int[][] directionsWithCheats = { [0, +2], [+2, 0], [0, -2], [-2, 0] };

            // Fill the char array and find start (S) and end (E) points
            for (int i = 0; i < lineCount; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    fileCharArray[i, j] = lines[i][j];

                    if (fileCharArray[i, j] == 'S')
                        startPoint.Add((i, j));
                    if (fileCharArray[i, j] == 'E')
                        finishPoint.Add((i, j));
                }
            }

            List<(int, int)> track = new List<(int, int)> { startPoint[0] };

            for (int i = 0; track[track.Count - 1] != finishPoint[0]; i++)
            {
                if (track[i] == finishPoint[0])
                    break;

                // Explore all 4 directions
                for (int j = 0; j < directions.Length; j++)
                {
                    int xMove = track[i].Item1 + directions[j][0];
                    int yMove = track[i].Item2 + directions[j][1];

                    if (xMove >= 0 && xMove < lineCount && yMove >= 0 && yMove < lineSymbolCount &&
                        fileCharArray[xMove, yMove] != '#' &&
                        !track.Contains((xMove, yMove)))
                    {
                        track.Add((xMove, yMove));
                    }
                }
            }

            Console.WriteLine(track.Count);
            int totalCheats = 0;

            for (int i = 0; i < track.Count; i++)
            {
                for (int j = 0; j < directionsWithCheats.Length; j++)
                {
                    int xMove = track[i].Item1 + directionsWithCheats[j][0];
                    int yMove = track[i].Item2 + directionsWithCheats[j][1];

                    if (xMove >= 0 && xMove < track.Count && yMove >= 0 && yMove < track.Count)
                    {
                        if (track.Contains((xMove, yMove)))
                        {
                            int trackIndex = track.IndexOf(track[i]);
                            int coordIndex = track.IndexOf((xMove, yMove));

                            if (trackIndex - coordIndex + 2 >= 100)
                                totalCheats++;
                        }

                    }
                }
            }

            Console.WriteLine(totalCheats);

            /*Console.WriteLine("Start Points:");
            foreach (var (x, y) in startPoint)
            {
                Console.WriteLine($"S at ({x}, {y})");
            }

            Console.WriteLine("\nFinish Points:");
            foreach (var (x, y) in finishPoint)
            {
                Console.WriteLine($"E at ({x}, {y})");
            }*/



        }
    }
}
