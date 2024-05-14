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

        public ObservableCollection<IBall> Balls { get; set; } = new ObservableCollection<IBall>();
        public string BallCount { get; set; } = "";
        LogicAPI logicAPI = LogicAPI.getInstance();



        public void restart()
        {
            start(int.Parse(BallCount));
        }

        private void start(int ballCount)
        {
            ObservableCollection<IBall> balls = logicAPI.Init(ballCount);
            Balls.Clear();
            foreach (IBall b in balls)
            {
                Balls.Add(b);
            }

        }
    }
}
