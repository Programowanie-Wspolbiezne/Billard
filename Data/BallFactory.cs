using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BallFactory
    {
        public static Data.IBall createBall(double radius, double x, double y)
        {
            Data.Ball ball =  new Data.Ball(radius);
            ball.X = x;
            ball.Y = y;
            return ball;
        }
    }
}
