using Snake;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Snake
{
    public class Visualizer
    {
        public static void GenerateBoard(char[,] frame)
        {
            Clear();
            for (int i = 0; i < frame.GetLength(0); i++)
            {
                Write(" |");
                for (int j = 0; j < frame.GetLength(1); j++)
                {
                    if (frame[i, j].ToString() == "\0") // default char value
                        Write("   |");
                    else
                    {
                        SetProperColor(frame[i, j]);
                        Write(" " + frame[i, j]);
                        ResetColor();
                        Write(" |");
                    }
                }
                Console.Write("\n\n");
            }
        }
        public static void SetProperColor(char myChar)
        {
            switch (myChar) 
            {
                case 'O':
                    ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case 'o':
                    ForegroundColor = ConsoleColor.Blue;
                    break;
                case 'X':
                    ForegroundColor = ConsoleColor.Red;
                    break;
            }
        }
    }
}
