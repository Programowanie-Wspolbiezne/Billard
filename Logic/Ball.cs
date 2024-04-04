using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Ball : Data.Ball
    {
        Timer timer;
        public Ball(float radius) : base(radius)
        {
            X = new Random().NextDouble() * 100;
            Y = new Random().NextDouble() * 100;
            timer = new Timer(x => Move(), null, 0, 100);
        }
       

        private void Move()
        {
            X += new Random().NextDouble() * 10 - 5;
            Y += new Random().NextDouble() * 10 - 5;
            System.Diagnostics.Debug.WriteLine(X + " " + Y);
        }
        
    }
}
