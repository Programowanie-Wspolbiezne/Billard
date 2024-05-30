using System.ComponentModel;
using System.Numerics;

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
