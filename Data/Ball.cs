using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : INotifyPropertyChanged,Data.IBall
    {
        private float radius;

        public Ball(float radius)
        {
            this.radius = radius;
        }

        private HashSet<IObserver<Ball>> observers = new HashSet<IObserver<Ball>>();

        private double x;
       
        //should this be observable or should all of that
        //be moved to logic
        public double X { get {
                return x;
            }
             set {
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
             set
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
