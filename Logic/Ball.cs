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
            //timer = new Timer(x => Move(), null, 0, 40);
            thread = new Thread(new ThreadStart(Move));
            thread.Start();

            velocity = new Vector2(new Random().NextSingle(), new Random().NextSingle());
            velocity = Vector2.Normalize(velocity);
        }


        private Thread thread;

        private Vector2 velocity;
        
       
        private void Move()
        {
            while (true)
            {
                if (dBall.X + velocity.X <= 0  || dBall.X + velocity.X >= 590) {
                    velocity.X = -velocity.X;
                }
                if (dBall.Y + velocity.Y <= 0 || dBall.Y + velocity.Y >= 290)
                {
                    velocity.Y = -velocity.Y;
                }

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



        public double X { get { return dBall.X; } }
        public double Y { get { return dBall.Y; } }

       
    }
}
