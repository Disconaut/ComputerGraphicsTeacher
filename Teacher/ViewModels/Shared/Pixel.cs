using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Teacher.ViewModels.Shared
{
    public readonly struct Pixel
    {
        public Pixel(int color, int index, int width)
        {
            Color = color;
            Index = index;
            Point = new Point(index % width, index / width);
        }

        public int Color { get; }

        public int Index { get; }

        public Point Point { get; }
    }
}
