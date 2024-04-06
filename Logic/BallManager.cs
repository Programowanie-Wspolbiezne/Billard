using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class BallManager : IBallManager
    {
        public Ball CreateBall(int x, int y, float radius)
        {
            Ball ball = new Ball();
            return ball;
        }
    }
}
