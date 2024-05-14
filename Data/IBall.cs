using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IBall : INotifyPropertyChanged
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double R { get; set; }
        public int Mass { get; set; }
        public Vector2 Velocity { get; set; }

    }
}
