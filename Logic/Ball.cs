using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Ball : Logic.IBall
    {
        Timer timer;
        Data.IBall dBall;
        
        public Ball(Data.IBall _dBall) 
        {
            dBall = _dBall;
            //I left INotifyPropertyChanged elements in Data, but they could be completly moved here
            dBall.PropertyChanged += OnDballPropertyChange;
            timer = new Timer(x => Move(), null, 0, 40);
        }

        private double targetX { get; set; }
        private double targetY { get; set; }

        private HashSet<IObserver<Ball>> observers = new HashSet<IObserver<Ball>>();

        public void AddForce(Vector2 force)
        {
            //And it is already an issue
            //X = force.X;
            //Y = force.Y;
        }

        void IBall.AddForce(Vector2 force)
        {
            throw new NotImplementedException();
        }

        private double distanceToTarget()
        {
            return Math.Sqrt(Math.Pow(targetX - X, 2) + Math.Pow(targetY - Y, 2));
        }


        private void MoveTowardsTarget()
        {
            double speed = 2;
            double dist = distanceToTarget();
            if (speed >= dist)
            {
                dBall.X = targetX;
                dBall.Y = targetY;
                return;
            }
            double progress = speed / dist;
            dBall.X = dBall.X * (1 - progress) + targetX * progress;
            dBall.Y = dBall.Y * (1 - progress) + targetY * progress;
        }
        private void Move()
        {
            if (targetX == X && targetY == Y)
            {
                //this should be linked to current canvas size
                targetX = (new Random().NextDouble() * 246);
                targetY = (new Random().NextDouble() * 250);
            }


            MoveTowardsTarget();
            System.Diagnostics.Debug.WriteLine(X + " " + Y);
        }


        private void OnDballPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            foreach (var observer in observers)
            {
                observer.OnNext(this);
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public double X { get { return dBall.X; } }
        public double Y { get { return dBall.Y; } }

       
    }
}
