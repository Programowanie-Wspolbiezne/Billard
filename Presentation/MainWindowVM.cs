using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    internal class MainWindowVM
    {
        public Start Restart { get; set; }
        public MainWindowVM()
        {
            Restart = new Start(this);
        }

        public ObservableCollection<IBall> Balls { get; } = new ObservableCollection<IBall>();
        public string BallCount { get; set; } = "";


        public void restart()
        {
            start(int.Parse(BallCount));
        }

        private void start(int ballCount)
        {
            Balls.Clear();
            ColisionDetector detector = new ColisionDetector();

            for (int i = 0; i < ballCount; i++)
            {
                IBall ball = BallFactory.CreateBall(new Random().NextDouble() * 590, new Random().NextDouble() * 290, 0);
                ball.R = 10;
                Balls.Add(ball);
                detector.addBall(ball);
            }
            
            detector.activate();
        }
    }
}
