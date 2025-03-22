namespace Alg2CourseWorkDay20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = Directory.GetCurrentDirectory() + "C:\\..\\..\\..\\input.txt";

            string[] lineRows = File.ReadAllLines(filePath);
            int rowCount = lineRows.Length;
            int colCount = lineRows[0].Length;

            char[,] lineCols = new char[rowCount, colCount];
            List<(int, int)> startPoint = new List<(int, int)>();
            List<(int, int)> finishPoint = new List<(int, int)>();

            int[][] directions = { [0, +1], [0, -1], [-1, 0], [+1, 0] };
            int[][] directionsWithCheats = { [0, +2], [+2, 0], [0, -2], [-2, 0] };

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    lineCols[i, j] = lineRows[i][j];

                    if (lineCols[i, j] == 'S')
                        startPoint.Add((i, j));
                    if (lineCols[i, j] == 'E')
                        finishPoint.Add((i, j));
                }
            }

            List<(int, int)> track = new List<(int, int)> { startPoint[0] };

            for (int i = 0; track[track.Count - 1] != finishPoint[0]; i++)
            {
                for (int j = 0; j < directions.Length; j++)
                {
                    int xMove = track[i].Item1 + directions[j][0];
                    int yMove = track[i].Item2 + directions[j][1];

                    if (xMove >= 0 && xMove < rowCount && yMove >= 0 && yMove < colCount &&
                        lineCols[xMove, yMove] != '#' &&
                        !track.Contains((xMove, yMove)))
                    {
                        track.Add((xMove, yMove));
                    }
                }
            }

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

            Console.WriteLine("ANSWER: To save atleast 100 picoseconds you need " + totalCheats + " cheats.");
        }
    }
}
