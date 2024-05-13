using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Snake
{
    public class Game
    {
        private MySnake Snake { get; set; }
        private Board Board { get; set; }
        private (int height, int width) Apple;
        public Game(MySnake snake, Board board)
        {
            this.Snake = snake;
            this.Board = board;
        }
        public void Run()
        {
            Thread ListeningThread = new Thread(() => KeyListener());
            ListeningThread.Start();
            GenerateApple();
            do
            {               
                
                var frame = GenerateFrame();
                Visualizer.GenerateBoard(frame);
                Thread.Sleep((int)Snake.SnakeSpeed);
                Snake.Move();
                DoesCollideWall();
            }
            while (Snake.Alive);
        }
        public void DoesCollideWall()
        {

            if (Snake.HeadPosition.width == Board.Width || Snake.HeadPosition.width < 0)
                Snake.Kill();
            if (Snake.HeadPosition.height == Board.Height || Snake.HeadPosition.height < 0)
                Snake.Kill();
        }
        public bool IsSnakeOn((int height,int width) cords) {
            var currentNode = Snake.SnakeBody.Last;
            while (currentNode != null)
            {
                if (currentNode.Value.height == cords.height && currentNode.Value.width == cords.width)
                    return true;
                currentNode = currentNode.Next;
            }
            return false;
        }
        public char[,] GenerateFrame()
        {
            char[,] matrix = new char[Board.Height, Board.Width];
            var node = Snake.SnakeBody.First;
            while (node != null)
            {
                matrix[node.Value.height, node.Value.width] = '#';
                node = node.Previous;
            }
            matrix[Apple.height, Apple.width] = 'X';
            return matrix;
        }
        public void GenerateApple() {
            var rnd = new Random();
            var cord = (rnd.Next(0, Board.Height), rnd.Next(0, Board.Width));
            if (IsSnakeOn(cord))
            {
                GenerateApple();
            }
            Apple = cord;
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
