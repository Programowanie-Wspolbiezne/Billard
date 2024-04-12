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
            GetNewTarget();
        }

        private double targetX { get; set; }
        private double targetY { get; set; }



        private double distanceToTarget()
        {
            return Math.Sqrt(Math.Pow(targetX - X, 2) + Math.Pow(targetY - Y, 2));
        }


        private void MoveTowardsTarget()
        {
            double speed = 10;
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
                GetNewTarget();
            }

            MoveTowardsTarget();
        }
        private void GetNewTarget()
        {
            if (new Random().NextDouble() > 0.5)
            {
                targetX = (new Random().NextDouble() * 590);
                targetY = (new Random().Next(2) * 290);
            }
            else
            {
                targetY = (new Random().NextDouble() * 290);
                targetX = (new Random().Next(2) * 590);
            }
        }

        private void OnDballPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
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
