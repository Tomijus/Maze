using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Maze
{
    class Functions
    {
        public static void PrintMaze(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    //Console.Write(matrix[i, j] + "\t");
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void CreateLogFile(string path)
        {
            //new StreamWriter(path);
            FileStream log = File.Open(path, FileMode.Create);
            log.Close();
        }

        public static void AddToLogFile(string comment)
        {
            StreamWriter log = new StreamWriter(@"C:\Users\Tomas\Downloads\task for RPA candidate\task for candidate\Log.txt", append: true);
            log.WriteLine(comment);
            log.Close();
        }

        public static Tuple<int, int> GetStartPositionCoordinates(int[,] matrix)
        {
            int i = 0;
            int j = 0;
            for (i = 0; i < matrix.GetLength(0); i++)
            {
                for (j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 2)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }
            return Tuple.Create(i, j);
        }
    }
}
