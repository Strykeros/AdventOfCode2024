namespace Alg2CourseWorkDay20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // get full file path
            string filePath = Directory.GetCurrentDirectory() + "C:\\..\\..\\..\\input.txt";

            // read the whole file and place it in string array, seperated into lines
            string[] lineRows = File.ReadAllLines(filePath);

            // get total number of rows and columns
            int rowCount = lineRows.Length;
            int colCount = lineRows[0].Length;

            // create lists that will store the start point and endpoint coordinates
            List<(int, int)> startPoint = new List<(int, int)>();
            List<(int, int)> finishPoint = new List<(int, int)>();

            // create arrays that store movements (up, down, left, right)
            int[][] directions = { [0, +1], [0, -1], [-1, 0], [+1, 0] };
            // create arrays that store cheat movements (up, down, left, right)
            int[][] directionsWithCheats = { [0, +2], [+2, 0], [0, -2], [-2, 0] };

            // loop through the lines
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    // if the start point is found ("S"), save its coordinates to the list
                    if (lineRows[i][j] == 'S')
                        startPoint.Add((i, j));
                    // if the finish point is found ("E"), save its coordinates to the list
                    if (lineRows[i][j] == 'E')
                        finishPoint.Add((i, j));
                }
            }

            // create list that will store the track coordinates and insert the start point 
            List<(int, int)> track = new List<(int, int)> { startPoint[0] };

            // loop until we get to the finish coords
            for (int i = 0; track[track.Count - 1] != finishPoint[0]; i++)
            {
                // check all directions
                for (int j = 0; j < directions.Length; j++)
                {
                    /* create a new x and y position.
                                                       X    Y
                     * Example: if the start point is [10, 12], we add 1 movement up (10+1),
                     * so the new position is [11, 12].
                    */
                    int xMove = track[i].Item1 + directions[j][0];
                    int yMove = track[i].Item2 + directions[j][1];

                    /* check if the coordinates are not outside of bounds, not a wall (#) and 
                     * make sure that we "don't go backwards"
                     */
                    if (xMove >= 0 && xMove < rowCount && yMove >= 0 && yMove < colCount &&
                        lineRows[xMove][yMove] != '#' &&
                        !track.Contains((xMove, yMove)))
                    {
                        // add the coordinates to list
                        track.Add((xMove, yMove));
                    }
                }
            }
            
            // create cheat counter
            int totalCheats = 0;

            // go through the whole track again 
            for (int i = 0; i < track.Count; i++)
            {
                // check directions with cheats
                for (int j = 0; j < directionsWithCheats.Length; j++)
                {
                    /* create a new x and y position with cheats.
                                                      X    Y
                    * Example: if the start point is [10, 12], we add 2 movements up (10+2),
                    * so the new position is [12, 12].
                    */
                    int xMove = track[i].Item1 + directionsWithCheats[j][0];
                    int yMove = track[i].Item2 + directionsWithCheats[j][1];

                    // check if the new coordinates are not outside of bounds
                    if (xMove >= 0 && xMove < track.Count && yMove >= 0 && yMove < track.Count)
                    {
                        // check if the new coordinates are on the track
                        if (track.Contains((xMove, yMove)))
                        {
                            // get current position index
                            int currentPosIndex = i;
                            // get position of index that we want to get to (destination)
                            int targetPosIndex = track.IndexOf((xMove, yMove));

                            // check if the cheat would save at least 100 seconds,
                            // from current position to destination.
                            if (targetPosIndex - currentPosIndex >= 100)
                                totalCheats++;
                        }

                    }
                }
            }

            // print answer
            Console.WriteLine("ANSWER: To save atleast 100 picoseconds you need " + totalCheats + " cheats.");
        }
    }
}
