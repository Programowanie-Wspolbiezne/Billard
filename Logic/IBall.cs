using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IBall : INotifyPropertyChanged
    {
        public double X { get; }
        public double Y { get; }
        public Vector2 Velocity { get; set; }
        public int Mass {  get; set; }
        public int R {  get; set; }
    }
}
