using System.ComponentModel;
using System.Numerics;

namespace Logic
{
    public interface IBall : INotifyPropertyChanged
    {
        public double X { get; }
        public double Y { get; }
        public Vector2 Velocity { get; set; }
        public int Mass {  get; set; }
        public double R {  get; set; }
        public void Kill() { }

        public void Start() { }
    }
}
