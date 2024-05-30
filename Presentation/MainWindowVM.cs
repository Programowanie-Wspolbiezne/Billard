using Logic;
using System.Collections.ObjectModel;

namespace Presentation
{
    internal class MainWindowVM
    {
        public Start Restart { get; set; }
        public MainWindowVM()
        {
            Restart = new Start(this);
        }

        public ObservableCollection<IBall> Balls { get; set; } = [];
        public string BallCount { get; set; } = "";
        readonly LogicAPI logicAPI = LogicAPI.GetInstance();



        public void RestartGame()
        {
            Start(int.Parse(BallCount));
        }

        private void Start(int ballCount)
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
