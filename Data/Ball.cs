using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ball : INotifyPropertyChanged
    {
        private float radius;
        Timer timer;

        public Ball(float radius)
        {
            this.radius = radius;
            timer = new Timer(x => Move(), null, 0, 100);

        }
        private void Move()
        {
            X += new Random().NextDouble() * 10 - 5;
            Y += new Random().NextDouble() * 10 - 5;
            System.Diagnostics.Debug.WriteLine(X + " " + Y);
        }

        private HashSet<IObserver<Ball>> observers = new HashSet<IObserver<Ball>>();

        private double x;
        public double X { get {
                return x;
            }
            protected set {
                x = value;
                OnPropertyChanged(nameof(X));
                foreach (var observer in observers) {
                    observer.OnNext(this);
                        }
            }
        }
        private double y;
        public double Y
        {
            get
            {
                return y;
            }
            protected set
            {
                y = value;
                OnPropertyChanged(nameof(Y));

                foreach (var observer in observers)
                {
                    observer.OnNext(this);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

}
