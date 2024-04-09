﻿using System;
using System.Collections;
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
            timer = new Timer(x => Move(), null, 0, 40);

        }

        private double distanceToTarget() {
            return Math.Sqrt(Math.Pow(targetX-X,2)+ Math.Pow(targetY - Y, 2));
        }



        private void MoveTowardsTarget()
        {
            double speed = 2;
            double dist = distanceToTarget();
            if (speed > dist) {
                X = targetX;
                Y = targetY;
                return;
            }
            double progress = speed / dist;
            X = X * (1 - progress) + targetX * progress;
            Y = Y * (1 - progress) + targetY * progress;
        }
        private void Move()
        {
            if(targetX == X && targetY == Y)
            {
                //this should be linked to current canvas size
                targetX = (new Random().NextDouble() * 246);
                targetY = (new Random().NextDouble() * 250);
            }


            MoveTowardsTarget();
            System.Diagnostics.Debug.WriteLine(X + " " + Y);
        }

        private HashSet<IObserver<Ball>> observers = new HashSet<IObserver<Ball>>();

        private double x;
        private double targetX {  get; set; }
        private double targetY { get; set; }
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
