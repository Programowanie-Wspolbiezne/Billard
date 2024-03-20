using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Board(int width, int height, float friction)
    {
        private float friction = friction;
        private int width = width;
        private int height = height;
        private List<Ball> balls = [];
    }
}
