﻿using System;
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
        public void Run()
        {
            Thread ListeningThread = new Thread(() => KeyListener());
            var frame = Visualizer.GenerateFrame(Snake, Board);
            Visualizer.GenerateBoard(frame);
            while (Snake.Alive)
            {
                Thread.Sleep((int)Snake.SnakeSpeed);
            }
        }
        public bool DoesCollide()
        {
            
            if (Snake.HeadPosition.width == Board.Width - 1 || Snake.HeadPosition.width < 0)
                return true;
            if (Snake.HeadPosition.height == Board.Height - 1 || Snake.HeadPosition.height < 0)
                return true;
            return false;
        }
        private void KeyListener()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                ChangeCoords(key);
            }
            while (key.Key != ConsoleKey.Escape);
        }
        private void ChangeCoords(ConsoleKeyInfo key)
        {

            if (key.Key == ConsoleKey.W)
            {
                Snake.DirectionY = 1;
                Snake.DirectionX = 0;
            }
            else if (key.Key == ConsoleKey.S)
            {
                Snake.DirectionY = -1;
                Snake.DirectionX = 0;
            }
            else if (key.Key == ConsoleKey.A)
            {
                Snake.DirectionY = 0;
                Snake.DirectionX = 1;
            }
            else if (key.Key == ConsoleKey.D)
            {
                Snake.DirectionY = 0;
                Snake.DirectionX = -1;
            }
        }
        private void PrintBlock(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
    }
}
