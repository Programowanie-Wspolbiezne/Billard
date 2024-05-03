using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class ColisionDetector
    {
        public ColisionDetector() {
            

        }
        private void colide()
        {
            while (true)
            {
                foreach (var ball in _balls)
                {
                    foreach (var ball2 in _balls)
                    {
                        if (ball2 != ball)
                        {
                            if (distance(ball, ball2) <= 2 * ball.R)
                            {
                                colide(ball, ball2);
                            }
                        }
                    }
                }
                
            }
                
        }

        private double distance(IBall ball, IBall ball2)
        {
            return Math.Sqrt((ball.X - ball2.X) * (ball.X - ball2.X) + (ball.Y - ball2.Y) * (ball.Y - ball2.Y));
        }

        private void colide(IBall b1, IBall b2)
        {       
            Vector2 vel1 = (b1.Mass - b2.Mass) / (b1.Mass + b2.Mass) * b1.Velocity + 2 * b2.Mass / (b1.Mass + b2.Mass) * b2.Velocity;
            Vector2 vel2 = (b1.Mass - b2.Mass) / (b1.Mass + b2.Mass) * b2.Velocity + 2 * b2.Mass / (b1.Mass + b2.Mass) * b1.Velocity;

            b1.Velocity = vel1;
            b2.Velocity = vel2;        

        }

        private Collection<IBall> _balls = new Collection<IBall>();
        public void addBall(IBall ball)
        {
            _balls.Add(ball);
        }

        public void activate()
        {
            new Thread(new ThreadStart(colide)).Start();
        }
    }


}
