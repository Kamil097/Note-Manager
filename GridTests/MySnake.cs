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
        public (int height, int width) HeadPosition => (SnakeBody.Last.Value.height, SnakeBody.Last.Value.width);


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
        public void Move()
        {
            var x = HeadPosition.width;
            var y = HeadPosition.height;
            switch (SnakeDirection)
            {
                case Direction.Up:
                    y += 1;
                    break;
                case Direction.Down:
                    y -= 1;
                    break;
                case Direction.Left:
                    x -= 1;
                    break;
                case Direction.Right:
                    x += 1;
                    break;
            }
            SnakeBody.AddFirst((y, x));
            SnakeBody.Remove(SnakeBody.Last);
            //_headPosition = (HeadPosition.Width + DirectionX, HeadPosition.Height + DirectionY);
        }
    }
}
