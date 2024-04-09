using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BallFactory
    {
        public static Data.IBall createBall(int radius)
        {
            return new Data.Ball(radius);
        }
    }
}
