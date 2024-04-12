using Data;
using Logic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ball = Logic.Ball;
using IBall = Logic.IBall;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBallManager ballManager;
        public ObservableCollection<IBall> Balls { get; } = new ObservableCollection<IBall>();
        public string BallCount {  get; set; } 

        public MainWindow(IBallManager ballManager)
        {
            this.ballManager = ballManager;
            DataContext = this;
            InitializeComponent();
            
            
        }
        private void restart(object sender, RoutedEventArgs e)
        {
            start(int.Parse(BallCount));
        }

        private void start(int ballCount)
        {
            Balls.Clear();
            for (int i = 0; i < ballCount; i++)
            {
                Balls.Add(ballManager.CreateBall(new Random().NextDouble() * 590, new Random().NextDouble() * 290, 0));
            }
        }

    }
}