using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze
{
    class Maze
    {
        public static void StartMaze()
        {
            int counter = 0;
            string line;
            int width = 0;
            int height = 0;
            int[,] maze = null;
            List<string> lineItems;
            Tuple<int, int> startPositionCoordinates;
            int x;
            int y;

            try
            {
                Functions.CreateLogFile(@"..\..\..\log.txt");
                System.IO.StreamReader file =
            new System.IO.StreamReader(@"..\..\..\RPAMaze.txt");
                while ((line = file.ReadLine()) != null)
                {
                    if (counter == 0)
                    {
                        height = Convert.ToInt32(line.Split(" ")[0]);
                        width = Convert.ToInt32(line.Split(" ")[1]);
                        maze = new int[height, width];
                    }
                    else if (counter <= width)
                    {
                        lineItems = line.Split(" ").ToList();
                        int xcounter = 0;
                        foreach (string item in lineItems)
                        {
                            maze[counter - 1, xcounter] = Int32.Parse(item);
                            xcounter++;
                        }
                    }
                    counter++;
                }
                file.Close();
                startPositionCoordinates = Functions.GetStartPositionCoordinates(maze);
                x = startPositionCoordinates.Item1;
                y = startPositionCoordinates.Item2;

                Functions.PrintMaze(maze);
                Console.WriteLine("Click ENTER to start");
                Console.WriteLine("Click ESCAPE to change start position.");

                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Functions.PrintMaze(maze);
                    Console.WriteLine("Enter new start position. X for vertical movement, Y for horizontal. (Posible range: 1-{0})", width);
                    Console.WriteLine("NOTE: start position can be only on path tile!");
                    Console.WriteLine("X=");
                    int newx = Convert.ToInt32(Console.ReadLine()) - 1;
                    Console.WriteLine("Y=");
                    int newy = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (maze[newx, newy] == 0)
                    {
                        maze[x, y] = 0;
                        maze[newx, newy] = 2;
                        //write to log
                        Functions.AddToLogFile(DateTime.Now.ToString() + " Changed start position. Old coordinates: x=" + x + ", y=" + y + ". New: x=" + newx + ", y=" + newy + "." + Environment.NewLine);
                        x = newx;
                        y = newy;

                    }
                    else
                    {
                        Console.WriteLine("This position is not possible.");
                        Environment.Exit(1);
                    }

                }
                else
                {
                    //write to log
                    Functions.AddToLogFile(DateTime.Now.ToString() + " Start position coordinates: x=" + x + ", y=" + y + Environment.NewLine);
                }

                Console.Clear();
                Functions.PrintMaze(maze);

                do
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.DownArrow:
                            //[+1, 0]
                            if (maze[x + 1, y] == 0)
                            {
                                maze[x, y] = 0;
                                x += 1;
                                maze[x, y] = 2;
                                Console.Clear();
                                Functions.PrintMaze(maze);
                                //write to log
                                Functions.AddToLogFile(DateTime.Now.ToString() + "[" + x + "," + y + "] Move down. " + Environment.NewLine);
                            }
                            else
                            {
                                Console.WriteLine("Incorrect move. Try again!");
                                //write to log
                                Functions.AddToLogFile(DateTime.Now.ToString() + "[" + (x + 1) + "," + y + "] Incorrect move. Try again!" + Environment.NewLine);
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            //[-1, 0]
                            if (maze[x - 1, y] == 0)
                            {
                                maze[x, y] = 0;
                                x -= 1;
                                maze[x, y] = 2;
                                Console.Clear();
                                Functions.PrintMaze(maze);
                                //write to log
                                Functions.AddToLogFile(DateTime.Now.ToString() + "[" + x + "," + y + "] Move up." + Environment.NewLine);
                            }
                            else
                            {
                                Console.WriteLine("Incorrect move. Try again!");
                                //write to log
                                Functions.AddToLogFile(DateTime.Now.ToString() + "[" + (x - 1) + "," + y + "] Incorrect move. Try again!" + Environment.NewLine);
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            //[0, +1]
                            if (maze[x, y + 1] == 0)
                            {
                                maze[x, y] = 0;
                                y += 1;
                                maze[x, y] = 2;
                                Console.Clear();
                                Functions.PrintMaze(maze);
                                //write to log
                                Functions.AddToLogFile(DateTime.Now.ToString() + "[" + x + "," + y + "] Move right." + Environment.NewLine);
                            }
                            else
                            {
                                Console.WriteLine("Incorrect move. Try again!");
                                //write to log
                                Functions.AddToLogFile(DateTime.Now.ToString() + "[" + x + "," + (y + 1) + "] Incorrect move. Try again!" + Environment.NewLine);
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            //[0, -1]
                            if (maze[x, y - 1] == 0)

                            {
                                maze[x, y] = 0;
                                y -= 1;
                                maze[x, y] = 2;
                                Console.Clear();
                                Functions.PrintMaze(maze);
                                //write to log
                                Functions.AddToLogFile(DateTime.Now.ToString() + "[" + x + "," + y + "] Move left." + Environment.NewLine);
                            }
                            else
                            {
                                Console.WriteLine("Incorrect move. Try again!");
                                //write to log
                                Functions.AddToLogFile(DateTime.Now.ToString() + "[" + x + "," + (y - 1) + "] Incorrect move. Try again!" + Environment.NewLine);
                            }
                            break;
                        default:
                            break;
                    }
                } while (x != 0 && y != 0 && x != width - 1 && y != width - 1);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
