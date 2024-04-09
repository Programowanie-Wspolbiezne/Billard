using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class BallManager : IBallManager
    {
        public IBall CreateBall(int x, int y, float radius)
        {
            IBall ball = new Ball(Data.BallFactory.createBall((int)radius));
            return ball;
        }
    }
}
