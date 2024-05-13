using Snake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Snake
{
    public static class Visualizer
    {
        public static void GenerateBoard(char[,] frame)
        {
            Clear();
            for (int i = 0; i < frame.GetLength(0); i++)
            {
                Console.Write(" |");
                for (int j = 0; j < frame.GetLength(1); j++)
                {
                    if (frame[i,j].ToString() == "\0") // default char value
                        Write("   |");
                    else
                        Write(" " + frame[i,j] + " |");
                }
                Console.Write("\n\n");
            }
        }
       
    }
}
