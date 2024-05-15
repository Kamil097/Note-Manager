using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class MySnake
    {
        public enum Speed {Slow = 500,Fast = 200, FastAsFuckBoi = 100}
        private Speed _speed { get; set; }
        public Speed SnakeSpeed => _speed;
        public enum Direction {Up,Down,Left,Right}
        public Direction SnakeDirection;
        private int _length { get; set; }
        public int Length => _length;
        private bool _alive { get; set; }
        public bool Alive => _alive;
        private LinkedList<(int, int)> _snakeBody;
        public LinkedList<(int height,int width)> SnakeBody => _snakeBody;
        public (int height, int width) HeadPosition => (SnakeBody.First.Value.height, SnakeBody.First.Value.width);


        public MySnake(Speed speed)
        {
            this._length = 1;
            this._alive = true;
            this._speed = speed;
            this._snakeBody = new LinkedList<(int, int)>(){};
            this.SnakeDirection = Direction.Right;
            _snakeBody.AddFirst((0, 0));
        }
        public void Kill() {
            this._alive = false;
        }
        public void Move(bool didEat)
        {
            var position = CalculateNextPos();
            SnakeBody.AddFirst(position);
            if(!didEat)
                SnakeBody.Remove(SnakeBody.Last);
        }
        public (int height, int width) CalculateNextPos() {
            var x = HeadPosition.width;
            var y = HeadPosition.height;
            switch (SnakeDirection)
            {
                case Direction.Up:
                    y -= 1;
                    break;
                case Direction.Down:
                    y += 1;
                    break;
                case Direction.Left:
                    x -= 1;
                    break;
                case Direction.Right:
                    x += 1;
                    break;
            }
            return (y, x);
        }
    }
}
