using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Data;
using Microsoft.Extensions.Logging;

namespace Logic
{
    public abstract class LogicAPI
    {
        public abstract ObservableCollection<IBall> Init(int ballCount);

        public static LogicAPI getInstance()
        {
            return new LogicApiImp();
        }
  
        private class LogicApiImp : LogicAPI
        {
            ObservableCollection<IBall> Balls = new ObservableCollection<IBall>();
            ILoggerFactory Factory = new LoggerFactory();
            ColisionDetector ColisionDetector = new ColisionDetector(LoggerProvider.GetLogger());

            public override ObservableCollection<IBall> Init(int ballCount)
            {
                IBoard board = Data.BoardFactory.createBoard(600, 300);
                
                ColisionDetector.Board = board;

                foreach (var ball in Balls)
                {
                    ball.Kill();
                }
                Balls.Clear();
                ColisionDetector.deactivate();

                for (int i = 0; i < ballCount; i++)
                {
                    IBall ball = BallFactory.CreateBall(new Random().NextDouble() * 580, new Random().NextDouble() * 280, 10);
                    ColisionDetector.addBall(ball);
                
                    Balls.Add(ball);
                }

                ColisionDetector.activate();

                foreach (var ball in Balls)
                {
                    ball.Start();
                }

                return Balls;
            }
        }

    }
}
