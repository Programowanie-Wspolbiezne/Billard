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
            readonly ObservableCollection<IBall> Balls = [];
            readonly ColisionDetector ColisionDetector = new(LoggerProvider.GetLogger());

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
