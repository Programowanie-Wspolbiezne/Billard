using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class BallFactory
    {
        public static IBall CreateBall(double x, double y, double radius)
        {
            IBall ball = new Ball(Data.BallFactory.createBall(radius, x, y));
            return ball;
        }
    }
}
