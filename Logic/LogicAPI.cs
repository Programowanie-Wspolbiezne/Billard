using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Data;

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
            ColisionDetector ColisionDetector = new ColisionDetector();

            public override ObservableCollection<IBall> Init(int ballCount)
            {
                IBoard board = Data.BoardFactory.createBoard(600, 300);
                ColisionDetector.deactivate();
                ColisionDetector.Board = board;
                Balls.Clear();
                for (int i = 0; i < ballCount; i++)
                {
                    IBall ball = BallFactory.CreateBall(new Random().NextDouble() * 590, new Random().NextDouble() * 290, 10);
                    ColisionDetector.addBall(ball);
                
                    Balls.Add(ball);


                }

                ColisionDetector.activate();
                return Balls;
            }
        }

    }
}
