using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Board(int width, int height)
    {
        public int Width { get; set; } = width; 
        public int Height { get; set;} = height;
    }
}
