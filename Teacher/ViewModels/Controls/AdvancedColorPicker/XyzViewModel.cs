using System;
using System.Collections.Generic;
using Windows.UI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.ColorModels;

namespace Teacher.ViewModels.Controls.AdvancedColorPicker
{
    class XyzViewModel : ColorModelViewModelBase
    {
        public override string Name => "XYZ";

        public override Color RgbColor 
        {
            get => _color.xyz2rgb(); 
            set => Color=XYZ.rgb2xyz(value); 
        }

        private XYZ _color;

        public XyzViewModel(Color color)
        {
            _color = XYZ.rgb2xyz(color);
        }

        public XYZ Color
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

        public byte X
        {
            get => _color._X;
            set
            {
                if (_color._X != value)
                {
                    _color._X = value;
                    OnPropertyChanged(nameof(_color._X));
                }
            }
        }
        public byte Y
        {
            get => _color._Y;
            set
            {
                if (_color._Y != value)
                {
                    _color._Y = value;
                    OnPropertyChanged(nameof(_color._Y));
                }
            }
        }
        public byte Z
        {
            set
            {
                if (_color._Z != value)
                {
                    _color._Z = value;
                    OnPropertyChanged(nameof(_color._Z));
                }
            }
            get => _color._Z;

        }
    }
}
