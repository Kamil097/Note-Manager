using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Game
    {
        private MySnake Snake { get; set; }
        private Board Board { get; set; }
        public Game(MySnake snake, Board board)
        {
            this.Snake = snake;
            this.Board = board;
        }
        public void Run()
        {
            Thread ListeningThread = new Thread(() => KeyListener());
            ListeningThread.Start();
            do
            {               
                
                var frame = Visualizer.GenerateFrame(Snake, Board);
                Visualizer.GenerateBoard(frame);
                Thread.Sleep((int)Snake.SnakeSpeed);
                Snake.Move();
                DoesCollide();
            }
            while (Snake.Alive);
        }
        public void DoesCollide()
        {

            if (Snake.HeadPosition.width == Board.Width  || Snake.HeadPosition.width < 0)
                Snake.Kill();
            if (Snake.HeadPosition.height == Board.Height  || Snake.HeadPosition.height < 0)
                Snake.Kill();
        }
        private void KeyListener()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                ChangeDirection(key);
            }
            while (key.Key != ConsoleKey.Escape);
        }
        private void ChangeDirection(ConsoleKeyInfo key)
        {

            if (key.Key == ConsoleKey.W)
            {
                Snake.SnakeDirection = MySnake.Direction.Up;
            }
            else if (key.Key == ConsoleKey.S)
            {
                Snake.SnakeDirection = MySnake.Direction.Down;
            }
            else if (key.Key == ConsoleKey.A)
            {
                Snake.SnakeDirection = MySnake.Direction.Left;
            }
            else if (key.Key == ConsoleKey.D)
            {
                Snake.SnakeDirection = MySnake.Direction.Right;
            }
        }
        private void PrintBlock(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
    }
}
