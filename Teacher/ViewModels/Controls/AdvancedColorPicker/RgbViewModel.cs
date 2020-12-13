using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Windows.Globalization.NumberFormatting;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Microsoft.UI.Xaml.Controls;
using Teacher.Attributes;

namespace Teacher.ViewModels.Controls.AdvancedColorPicker
{
    class RgbViewModel : ColorModelViewModelBase
    {
        public override string Name => "RGB";

        public override Color RgbColor
        {
            get => Color;
            set => Color = value;
        }

        public override void SetRgbColorWithoutNotification(Color color)
        {
            _color = color;
        }

        private Color _color;

        public RgbViewModel()
        {
        }

        public RgbViewModel(Color color)
        {
            _color = color;
        }

        public Color Color
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

        [UIElement(typeof(byte), Name = "Red")]
        [Range(0.0, 255.0)]
        public double R
        {
            get => _color.R;
            set
            {
                if (_color.R != (byte)value)
                {
                    _color.R = (byte)value;
                    OnPropertyChanged(nameof(RgbColor));
                }
            }
        }

        [UIElement(typeof(byte), Name = "Green")]
        [Range(0.0, 255.0)]
        public double G
        {
            get => _color.G;
            set
            {
                if (_color.G != (byte)value)
                {
                    _color.G = (byte)value;
                    OnPropertyChanged(nameof(RgbColor));
                }
            }
        }

        [UIElement(typeof(byte), Name = "Blue")]
        [Range(0.0, 255.0)]
        public double B
        {
            get => _color.B;
            set
            {
                if (_color.B != (byte)value)
                {
                    _color.B = (byte)value;
                    OnPropertyChanged(nameof(RgbColor));
                }
            }
        }
    }
}