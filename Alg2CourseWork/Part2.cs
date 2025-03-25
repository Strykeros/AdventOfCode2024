using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg2CourseWorkDay20
{
    internal class Part2
    {
        public Part2()
        {
            
        }

        public void Execute()
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

            // create array that store movements (up, down, left, right)
            int[][] directions = { [0, +1], [0, -1], [-1, 0], [+1, 0] };
            // create array that store cheat movements with vertical, horizontal, and step variations
            int[][] directionsWithCheats = { [-20, 0, 1], [-19, -1, 3], [-18, -2, 5], [-17, -3, 7], [-16, -4, 9], [-15, -5, 11], [-14, -6, 13], [-13, -7, 15], 
                                        [-12, -8, 17], [-11, -9, 19], [-10, -10, 21], [-9, -11, 23], [-8, -12, 25], [-7, -13, 27], [-6, -14, 29], [-5, -15, 31], 
                                        [-4, -16, 33], [-3, -17, 35], [-2, -18, 37], [-1, -19, 39], [0, -20, 41], 
                                        [20, 0, 1], [19, -1, 3], [18, -2, 5], [17, -3, 7], [16, -4, 9], [15, -5, 11], [14, -6, 13], [13, -7, 15], 
                                        [12, -8, 17], [11, -9, 19], [10, -10, 21], [9, -11, 23], [8, -12, 25], [7, -13, 27], [6, -14, 29], [5, -15, 31], 
                                        [4, -16, 33], [3, -17, 35], [2, -18, 37], [1, -19, 39] };

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

            // Create cheat counter
            int totalCheats = 0;

            Console.WriteLine("Loop running.....");

            // Loop through each step in the path
            for (int i = 0; i < track.Count; i++)
            {
                var step = track[i];
                Console.WriteLine($"STARTING STEP {i + 1} OF {track.Count}");

                // Go through each possible direction
                for (int j = 0; j < directionsWithCheats.Length; j++)
                {
                    var dir = directionsWithCheats[j];
                    int dirY = dir[0];

                    // Try all time intervals within this direction
                    for (int k = 0; k < dir[2]; k++)
                    {
                        /* Create a new x and y position by applying a cheat movement.
                         * The direction is defined by 'dir', which contains:
                         *    dir[0] = vertical movement (Y-axis)
                         *    dir[1] = starting horizontal movement (X-axis)
                         *    dir[2] = number of horizontal variations to test (used in the 'k' loop)
                         * 
                         * For each step in the path, we explore variations of horizontal movement (dirX) by increasing it with 'k'.
                         * 
                         * Example:
                         *   Current position = [10, 12] (Y, X)
                         *   dirY = -2 (move 2 up), dirX = -1 (move 1 left), k = 1 → total dirX = -1 + 1 = 0
                         *   New position = [10 + (-2), 12 + 0] = [8, 12]
                         */

                        int dirX = dir[1] + k;
                        int yMove = step.Item1 + dirY;
                        int xMove = step.Item2 + dirX;

                        // check if the new coordinates are not outside of bounds
                        if (xMove >= 0 && xMove < track.Count && yMove >= 0 && yMove < track.Count)
                        {

                            // Check if new position exists in path
                            if (track.Contains((yMove, xMove)))
                            {
                                // get current position index
                                int index0 = track.IndexOf(step);
                                // get position of index that we want to get to (destination)
                                int index1 = track.IndexOf((yMove, xMove));

                                // If the index difference minus the movement cost is >= 100, it's a cheat

                                // check if the cheat would save at least 100 seconds,
                                // from current position to destination.
                                if (index1 - (index0 + Math.Abs(dirX) + Math.Abs(dirY)) >= 100)
                                {
                                    totalCheats++;
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("PART 2 ANSWER: " + totalCheats);
        }

    }
}
