using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Tui
{
    public class Ball
    {
        public int x;
        public int y;
        private int speed;
        private int RightLeft;
        private int UpDown;
        public Ball(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.UpDown = RightLeft = 1;
            this.speed = 1;
        }
        public bool CalculateBall(int width, int height) 
        {
            this.x += RightLeft * RandomizeSpeed(5);  
            this.y +=  UpDown * RandomizeSpeed(3);
            ManageOverBounce(width, height);

            var bounce = false;
            if (x == 0) {
                RightLeft = 1; bounce = true; 
            }
            else if (x == width) { bounce = true;
                RightLeft = -1;
            }
            if (y == 0) {
                UpDown = 1;
            }
            if (y == height) {
                UpDown = -1;
            }


            return bounce;
        }
        private void ManageOverBounce(int width, int height) 
        {
            if (this.x > width) this.x = width;
            else if (this.x < 0) this.x = 0;
            if (this.y > height) this.y = height;
            else if (this.y < 0) this.y = 0; 
        }
        private int RandomizeSpeed(int maxSpeed) 
        {
            Random rnd = new Random();
            return rnd.Next(0, maxSpeed);
        }
        
    }
}
