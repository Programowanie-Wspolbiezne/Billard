using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Ball : Logic.IBall
    {
        Data.IBall dBall;
        
        public Ball(Data.IBall _dBall) 
        {
            dBall = _dBall;
            dBall.PropertyChanged += OnDballPropertyChange;
            thread = new Thread(new ThreadStart(Move));
            thread.Start();

            Velocity = new Vector2(new Random().NextSingle(), new Random().NextSingle());
            Velocity = Vector2.Normalize(velocity)/4;
            dBall.Mass = 1;
        }


        private Thread thread;

        private Vector2 velocity;

        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }


        public double X { get { return dBall.X; } }
        public double Y { get { return dBall.Y; } }

        public int Mass { get { return dBall.Mass; } set { dBall.Mass = value; } }

        public double R { get { return dBall.R; } set { dBall.R = value; } }


        private void Move()
        {
            while (true)
            {
               

                dBall.X += velocity.X;
                dBall.Y += velocity.Y;
                
                Thread.Sleep(1);
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



    }
}
