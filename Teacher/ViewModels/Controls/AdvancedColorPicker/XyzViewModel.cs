using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Windows.UI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.ColorModels;
using Colourful;
using Colourful.Conversion;
using Teacher.Attributes;

namespace Teacher.ViewModels.Controls.AdvancedColorPicker
{
    class XyzViewModel : ColorModelViewModelBase
    {
        public override string Name => "XYZ";

        public override Color RgbColor 
        {
            get => _color.xyz2rgb(); 
            set => Color = XyzColor.rgb2xyz(value); 
        }

        public override void SetRgbColorWithoutNotification(Color color)
        {
            _color = XyzColor.rgb2xyz(color);
        }

        private XyzColor _color;

        public XyzViewModel()
        {
        }

        public XyzViewModel(Color color)
        {
            Color = XyzColor.rgb2xyz(color);
        }

        public XyzColor Color
        {
            get => _color;
            set
            {
                if (_color != value) 
                {
                    _color = value;
                    OnPropertyChanged(string.Empty);
                }
            }
        }

        [UIElement(typeof(double), Name = "X")]
        [Range(0.0, 0.9505)]
        public double X
        {
            get => _color.X;
            set
            {
                if (!_color.X.Equals(value))
                {
                    _color.X = value;
                    OnPropertyChanged(nameof(RgbColor));
                }
            }
        }

        [UIElement(typeof(double), Name = "Y")]
        [Range(0.0, 1.0)]
        public double Y
        {
            get => _color.Y;
            set
            {
                if (!_color.Y.Equals(value))
                {
                    _color.Y = value;
                    OnPropertyChanged(nameof(RgbColor));
                }
            }
        }

        [UIElement(typeof(double), Name = "Z")]
        [Range(0.0, 1.089)]
        public double Z
        {
            set
            {
                if (!_color.Z.Equals(value))
                {
                    _color.Z = value;
                    OnPropertyChanged(nameof(RgbColor));
                }
            }
            get => _color.Z;
        }
    }
}
