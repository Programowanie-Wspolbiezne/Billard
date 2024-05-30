using System.ComponentModel;
using System.Numerics;

namespace Data
{
    internal class Ball(double radius) : INotifyPropertyChanged, IBall
    {

        public double R { get; set; } = radius;
        public Vector2 Velocity { get; set; }

        private double x;
       
        //should this be observable or should all of that
        //be moved to logic
        public double X { get {
                return x;
            }
             set {
                x = value;
                OnPropertyChanged(nameof(X));
            }
        }
        private double y;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
            y = value;
            OnPropertyChanged(nameof(Y));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Mass {  get; set; }
    }

}
