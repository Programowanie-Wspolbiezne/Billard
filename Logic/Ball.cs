using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Ball : Data.Ball, IBall
    {
        
        public Ball() : base(10)
        {
            X = new Random().NextDouble() * 100;
            Y = new Random().NextDouble() * 100;
        }

        public void AddForce(Vector2 force)
        {
            X = force.X;
            Y = force.Y;
        }


        
    }
}
