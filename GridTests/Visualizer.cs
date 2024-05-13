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
        public static void SnakeAnimation()
        {
            var length = 8;
            for (int i = 0; i < length; i++)
            {
                SetCursorPosition( i, 0);
                Write("X");
            }
            
            for (int x = 0; x < WindowWidth - 1 - length; x++)
            {
                SetCursorPosition(x,0);
                Write(" ");
                SetCursorPosition(x+length,0);
                Write("X");
                Thread.Sleep(50);
            }
        }
        public static void GenerateBoard(char[,] frame)
        {
            for (int i = 0; i < frame.GetLength(0); i++)
            {
                Console.Write(" |");
                for (int j = 0; j < frame.GetLength(1); j++)
                {
                    Console.Write(" " + frame[i,j] + " |");
                }
                Console.Write("\n\n");
            }
        }
        public static char[,] GenerateFrame(MySnake snake, Board board)
        {
            char[,] matrix = new char[board.Height, board.Width];
            var node = snake.SnakeBody.First;
            while(node!=null)
            {
                matrix[node.Value.height,node.Value.width] = '#';
                node = node.Previous;
            }
            return matrix;
        }
    }
}
