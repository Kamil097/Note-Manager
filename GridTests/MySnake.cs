using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class MySnake
    {
        public int DirectionX = 1;
        public int DirectionY = 0;  

        public enum Speed {Slow = 100,Fast = 50, FastAsFuckBoi = 20}
        private int _length { get; set; }
        public int Length => _length;
        private bool _alive { get; set; }
        public bool Alive => _alive;
        private Speed _speed { get; set; }
        public Speed SnakeSpeed => _speed;
        private LinkedList<(int, int)> _snakeBody;
        public LinkedList<(int height,int width)> SnakeBody => _snakeBody;
        public (int height, int width) HeadPosition => (SnakeBody.Last.Value.height, SnakeBody.Last.Value.width);


        public MySnake(Speed speed)
        {
            this._length = 1;
            this._alive = true;
            this._speed = speed;
            this._snakeBody = new LinkedList<(int, int)>(){};
            _snakeBody.AddFirst((0, 0));
        }
        public void Kill() {
            this._alive = false;
        }
        public void Move()
        {
            //_headPosition = (HeadPosition.Width + DirectionX, HeadPosition.Height + DirectionY);
        }
    }
}
