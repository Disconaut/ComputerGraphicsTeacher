using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace CGTeacherShared.ColorModels
{
    struct XYZ
    {
        public byte _X;
        public byte _Y;
        public byte _Z;
        public XYZ(double X, double Y, double Z)
        {
            _X = (byte)X;
            _Y = (byte)Y;
            _Z = (byte)Z;
        }

        public Color xyz2rgb()
        {
           return Color.FromArgb(
                (byte)255,
                converter(_X),
                converter(_Y), 
                converter(_Z)
                ); 
        }

        private static byte converter(byte C)
        {
            if (Math.Abs(C) < 0.0031308)
            {
                return (byte)(12.92 * C);
            }
            return (byte)(1.055 * Math.Pow(C, 0.41666) - 0.055);
        }

        public static XYZ rgb2xyz(Color color)
        {
            double rLinear = (double)color.R / 255.0;
            double gLinear = (double)color.G / 255.0;
            double bLinear = (double)color.B / 255.0;

            double r = (rLinear > 0.04045) ? 
                Math.Pow((rLinear + 0.055) / (
                1 + 0.055), 2.2) : (rLinear / 12.92);
            double g = (gLinear > 0.04045) ? 
                Math.Pow((gLinear + 0.055) / (
                1 + 0.055), 2.2) : (gLinear / 12.92);
            double b = (bLinear > 0.04045) ? 
                Math.Pow((bLinear + 0.055) / (
                1 + 0.055), 2.2) : (bLinear / 12.92);

            return new XYZ(
                (r*0.4124 + g*0.3576 + b*0.1805),
                (r*0.2126 + g*0.7152 + b*0.0722),
                (r*0.0193 + g*0.1192 + b * 0.9505)
            );

        }
    }
}
