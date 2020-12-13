using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Colourful;
using Colourful.Conversion;

namespace CGTeacherShared.ColorModels
{
    public struct XyzColor
    {
        private static ColourfulConverter _converter;

        public bool Equals(XyzColor other)
        {
            return Math.Abs(X - other.X) < 0.00001 && Math.Abs(Y - other.Y) < 0.00001 && Math.Abs(Z - other.Z) < 0.00001;
        }

        public override bool Equals(object obj)
        {
            return obj is XyzColor other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public XyzColor(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        static XyzColor()
        {
            _converter = new ColourfulConverter
            {
                WhitePoint = Illuminants.D65
            };
        }

        public Color xyz2rgb()
        {
            var colourfulXyz = new XYZColor(X, Y, Z);
            var colourfulRgb = _converter.ToRGB(colourfulXyz);
            return Color.FromArgb(
                255,
                (byte)(colourfulRgb.R * 255.0),
                (byte)(colourfulRgb.G * 255.0),
                (byte)(colourfulRgb.B * 255.0));
        }

        public static XyzColor rgb2xyz(Color color)
        {
            var colourfulRgb = new RGBColor(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            var colourfulXyz = _converter.ToXYZ(colourfulRgb);

            return new XyzColor(
                colourfulXyz.X,
                colourfulXyz.Y,
                colourfulXyz.Z
            );

        }
        public static bool operator ==(XyzColor left, XyzColor right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(XyzColor left, XyzColor right)
        {
            return !left.Equals(right);
        }
    }
}
