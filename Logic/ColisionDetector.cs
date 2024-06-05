using Data;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;

namespace Logic
{
    public class ColisionDetector()
    {
        private readonly Collection<IBall> _balls = [];
        private readonly Object lockk = new();
        public IBoard Board { get; set; }

        private void ColideEvent(object sender, PropertyChangedEventArgs e)
        {
            IBall ball = (IBall)sender;
            CheckBorder(ball);
            lock (lockk)
            {
                foreach (var ball2 in _balls)
                {
                    if (ball != ball2)
                    {
                        if (Distance(ball2, ball) <= 2 * ball.R)
                        {
                            Colide(ball2, ball);
                        }
                    }
                }
            }
                
        }

        private void CheckBorder(IBall ball)
        {
            if (ball.X + ball.Velocity.X <= 0 || ball.X + ball.Velocity.X >= Board.Width - 2 * ball.R)
            {
                ball.Velocity = new Vector2( -ball.Velocity.X, ball.Velocity.Y);
                //logger.LogInformation("Ball collided with vertical wall in X={X} Y={Y}",ball.X,ball.Y);
            }
            if (ball.Y + ball.Velocity.Y <= 0 || ball.Y + ball.Velocity.Y >= Board.Height - 2 * ball.R)
            {
                ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
               // logger.LogInformation("Ball collided with horizontal wall in X={X} Y={Y}", ball.X, ball.Y);
            }
        }

        private static double Distance(IBall ball, IBall ball2)
        {
            
            double xdif = (ball.X + ball.Velocity.X)  - (ball2.X + ball2.Velocity.X);
            double ydif = (ball.Y + ball.Velocity.Y) - (ball2.Y + ball2.Velocity.Y);
            return Math.Sqrt((xdif * xdif) + (ydif * ydif));
               
        }

        private static void Colide(IBall b1, IBall b2)
        {
            Vector2 vel1 = (b1.Mass - b2.Mass) / (b1.Mass + b2.Mass) * b1.Velocity + 2 * b2.Mass / (b1.Mass + b2.Mass) * b2.Velocity;
            Vector2 vel2 = (b1.Mass - b2.Mass) / (b1.Mass + b2.Mass) * b2.Velocity + 2 * b2.Mass / (b1.Mass + b2.Mass) * b1.Velocity;

            b1.Velocity = vel1;
            b2.Velocity = vel2;        

        }

        public void AddBall(IBall ball)
        {
            _balls.Add(ball);
        }

        public void Activate()
        {
            foreach (var ball in _balls)
            {
                ball.PropertyChanged += ColideEvent;
            }
        }
        public void Deactivate()
        {
            foreach (var ball in _balls)
            {
                ball.PropertyChanged -= ColideEvent;
            }
            _balls.Clear();
        }
    }


}
