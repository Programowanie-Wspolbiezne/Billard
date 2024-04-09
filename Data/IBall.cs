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
        double X { get; set; }
        double Y { get; set; }
    }
}
