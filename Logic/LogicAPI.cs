using System.Collections.ObjectModel;
using Data;
using Microsoft.Extensions.Logging;

namespace Logic
{
    public abstract class LogicAPI
    {

        public abstract ObservableCollection<IBall> Init(int ballCount);

        public static LogicAPI GetInstance()
        {
            return new LogicApiImp();
        }

        private class LogicApiImp : LogicAPI
        {
            private static ILogger logger = LoggerProvider.GetLogger();
            private static ObservableCollection<IBall> Balls = [];
            readonly ColisionDetector ColisionDetector = new();
            Timer timer = new Timer(LogBalls, null, 100, 10000);

            private static void LogBalls(object? state)
            {
                foreach (var ball in Balls)
                {
                    logger.LogInformation("Ball was in X={X} Y={Y} position", ball.X, ball.Y);
                }
            }

            public override ObservableCollection<IBall> Init(int ballCount)
            {
                IBoard board = BoardFactory.CreateBoard(600, 300);
                
                ColisionDetector.Board = board;

                foreach (var ball in Balls)
                {
                    ball.Kill();
                }
                Balls.Clear();
                ColisionDetector.Deactivate();

                for (int i = 0; i < ballCount; i++)
                {
                    IBall ball = BallFactory.CreateBall(new Random().NextDouble() * 580, new Random().NextDouble() * 280, 10);
                    ColisionDetector.AddBall(ball);
                
                    Balls.Add(ball);
                }

                ColisionDetector.Activate();

                foreach (var ball in Balls)
                {
                    ball.Start();
                }

                return Balls;
            }
        }

    }
}
