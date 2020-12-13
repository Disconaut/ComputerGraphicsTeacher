using Windows.Globalization.NumberFormatting;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Microsoft.UI.Xaml.Controls;

namespace Teacher.ViewModels.Controls.AdvancedColorPicker
{
    class RgbViewModel : ColorModelViewModelBase
    {
        public override string Name => "Hello";

        public override Color RgbColor
        {
            get => Color;
            set => Color = value;
        }

        private Color _color;

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
                    OnPropertyChanged();
                }
            }
        }

        public byte A
        {
            get => _color.A;
            set
            {
                if (_color.A != value)
                {
                    _color.A = value;
                    OnPropertyChanged(nameof(A));
                }
            }
        }
        
        public byte R
        {
            get => _color.R;
            set
            {
                if (_color.R != value)
                {
                    _color.R = value;
                    OnPropertyChanged(nameof(R));
                }
            }
        }
        
        public byte G
        {
            get => _color.G;
            set
            {
                if (_color.G != value)
                {
                    _color.G = value;
                    OnPropertyChanged(nameof(G));
                }
            }
        }
        
        public byte B
        {
            get => _color.B;
            set
            {
                if (_color.B != value)
                {
                    _color.B = value;
                    OnPropertyChanged(nameof(B));
                }
            }
        }

        
    }
}