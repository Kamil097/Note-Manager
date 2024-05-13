using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public (int, int) apple { get; set; }
        public Board(int width, int height)
        {
            this.Height = height;
            this.Width = width;
        }  
    }
}
