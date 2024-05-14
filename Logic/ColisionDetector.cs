using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class ColisionDetector
    {
        private Collection<IBall> _balls = new Collection<IBall>();
        private Object lockk = new object();
        public IBoard Board { get; set; }

        private void colide_event(object sender, PropertyChangedEventArgs e)
        {
            IBall ball = (IBall)sender;
            check_border(ball);
            lock (lockk)
            {
                
                foreach (var ball2 in _balls)
                {
                    if (ball != ball2)
                    {
                        if (distance(ball2, ball) <= 2 * ball.R)
                        {
                            colide(ball2, ball);
                        }
                    }
                }
            }
                
        }

        private void check_border(IBall ball)
        {
            if (ball.X + ball.Velocity.X <= 0 || ball.X + ball.Velocity.X >= Board.Width - 2 * ball.R)
            {
                ball.Velocity = new Vector2( -ball.Velocity.X, ball.Velocity.Y);
            }
            if (ball.Y + ball.Velocity.Y <= 0 || ball.Y + ball.Velocity.Y >= Board.Height - 2 * ball.R)
            {
                ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
            }
        }

        private static double distance(IBall ball, IBall ball2)
        {
            
            double xdif = (ball.X + ball.Velocity.X)  - (ball2.X + ball2.Velocity.X);
            double ydif = (ball.Y + ball.Velocity.Y) - (ball2.Y + ball2.Velocity.Y);
            return Math.Sqrt((xdif * xdif) + (ydif * ydif));
               
        }

        private static void colide(IBall b1, IBall b2)
        {
            Vector2 vel1 = (b1.Mass - b2.Mass) / (b1.Mass + b2.Mass) * b1.Velocity + 2 * b2.Mass / (b1.Mass + b2.Mass) * b2.Velocity;
            Vector2 vel2 = (b1.Mass - b2.Mass) / (b1.Mass + b2.Mass) * b2.Velocity + 2 * b2.Mass / (b1.Mass + b2.Mass) * b1.Velocity;

            b1.Velocity = vel1;
            b2.Velocity = vel2;        

        }

        public void addBall(IBall ball)
        {
            _balls.Add(ball);
        }

        public void activate()
        {
            foreach (var ball in _balls)
            {
                ball.PropertyChanged += colide_event;
            }
            //new Thread(new ThreadStart(colide)).Start();
        }
        public void deactivate()
        {
            foreach (var ball in _balls)
            {
                ball.PropertyChanged -= colide_event;
            }
            _balls.Clear();
        }
    }


}
