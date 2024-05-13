using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Pong.Tui
{
    public class Board
    {
        private Platform P1;
        private Platform P2;
        private Ball Ball;
        private int Score1 { get; set; }
        private int Score2 { get; set; }
        private int Height => WindowHeight-1;
        private int Width => WindowWidth-1;

        public void Run()
        {
            Initialize();
            Thread ListeningThread = new Thread(() => KeyListener());
            ListeningThread.Start();

            do
            {
                GenerateBoard();
                Clear();
                var bounce = Ball.CalculateBall(Width, Height);
                if (bounce)
                {
                    if ((bounce && Ball.x == 0) && (Ball.y > P1.LowerCorner || Ball.y < P1.UpperCorner)) {
                        Score2++;
                        break;
                    }
                    if ((bounce && Ball.x == Width) && (Ball.y > P2.LowerCorner || Ball.y < P2.UpperCorner)) {
                        Score1++;
                        break;
                    }
                }
            }
            while (true);
            Clear();
            WriteLine($"SCORE BOARD\nPLAYER1: {Score1} POINTS\nPLAYER2: {Score2} POINTS\nPress any key to continue...");
            ReadLine();
            Run();
           
        }
        private void KeyListener()
        {
            ConsoleKeyInfo key;
            do
            {
                key = ReadKey(true);
                ChangeCoords(key);
            }
            while (key.Key != ConsoleKey.Escape);
        }

        public void Initialize()
        {
            P1 = new Platform(Height / 2);
            P2 = new Platform(Height / 2);
            Ball = new Ball(Width / 2,Height / 2);
        }
        public void GenerateBoard()
        {
            for (int y = P1.UpperCorner; y<=P1.LowerCorner;y++)
            {
                PrintBlock(0, y);
            }
            for (int y = P2.UpperCorner; y <= P2.LowerCorner; y++)
            {
                PrintBlock(Width, y);
            }
            PrintBlock(Ball.x, Ball.y);
            Thread.Sleep(50);
            while (KeyAvailable)
            { //clears buffor
                ReadKey(true);
            }
        }
        private void ChangeCoords(ConsoleKeyInfo key)
        {
            
            if (key.Key == ConsoleKey.W && P1.UpperCorner >= 1)
            {
                P1.Center--;
            }
            else if (key.Key == ConsoleKey.S && P1.LowerCorner <= Height)
            {
                P1.Center++;
            }
            else if (key.Key == ConsoleKey.UpArrow && P2.UpperCorner >= 1)
            {
                P2.Center--;
            }
            else if (key.Key == ConsoleKey.DownArrow && P2.LowerCorner <= Height)
            {
                P2.Center++;
            }
        }
        private void PrintBlock(int x, int y)
        {
            SetCursorPosition(x, y);
            Write(@"■");
        }
    }
}
