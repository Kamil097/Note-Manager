using Snake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace GridTests
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
        public static void GenerateBoard(Board board)
        {
            SetCursorPosition(1,0);
            Write(new string("–"),board.Width);
            SetCursorPosition(1, board.Height-1);
            Write(new string("–"), board.Width);
            SetCursorPosition(0, 1);
            Write(new string("|\n"), board.Height);
            //SetCursorPosition(0, );
            Write(new string("|\n"), board.Height);
        }
    }
}
