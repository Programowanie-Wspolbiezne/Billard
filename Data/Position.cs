﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Position
    {
        
        public int x { get; private set; }
        private int y{ get; set; }
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
