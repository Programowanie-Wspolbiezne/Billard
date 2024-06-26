﻿using System.ComponentModel;
using System.Numerics;

namespace Logic
{
    public class Ball : IBall
    {
        readonly Data.IBall dBall;
        
        public Ball(Data.IBall _dBall) 
        {
            dBall = _dBall;
            dBall.PropertyChanged += OnDballPropertyChange;
            thread = new Thread(new ThreadStart(Move));
          

            Velocity = new Vector2(new Random().NextSingle(), new Random().NextSingle());
            Velocity = Vector2.Normalize(Velocity) /4;
            dBall.Mass = 1;
        }


        private readonly Thread thread;

        public Vector2 Velocity { get { return dBall.Velocity; } set { dBall.Velocity = value; } }
        public double X { get { return dBall.X; } }
        public double Y { get { return dBall.Y; } }
        public int Mass { get { return dBall.Mass; } set { dBall.Mass = value; } }
        public double R { get { return dBall.R; } set { dBall.R = value; } }

        private bool is_not_dead = true;
        private void Move()
        {
            while (is_not_dead)
            {
               
                dBall.X += Velocity.X;
                dBall.Y += Velocity.Y;
                
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

        public void Kill() {
            is_not_dead = false;
            thread.Join();
        }

        public void Start()
        {
            thread.Start();
        }

    }
}
