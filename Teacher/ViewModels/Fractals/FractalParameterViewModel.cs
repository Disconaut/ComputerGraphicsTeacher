using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Globalization.NumberFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using CGTeacherShared.Fractals.Abstract;
using Microsoft.UI.Xaml.Controls;
using Teacher.Controls;
using Windows.UI;

namespace Teacher.ViewModels.Fractals
{
    public class FractalParameterViewModel : INotifyPropertyChanged
    {
        private readonly IFractalParameter _fractalParameter;
        private readonly ResourceLoader _resourceLoader;

        public FractalParameterViewModel(IFractalParameter fractalParameter)
        {
            _fractalParameter = fractalParameter;
            _resourceLoader = ResourceLoader.GetForCurrentView();

            CreateAppropriateUiElement();
        }

        public string Name
        {
            get
            {
                var name = _resourceLoader.GetString(_fractalParameter.Name);
                return string.IsNullOrEmpty(name) ? _fractalParameter.Name : name;
            }
        }

        public Type Type => _fractalParameter.Type;

        public object Value
        {
            get => _fractalParameter.Value;
            set
            {
                if (_fractalParameter.Value?.Equals(value) ?? false) return;
                _fractalParameter.Value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public UIElement UiElement { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateAppropriateUiElement()
        {
            var bind = new Binding
            {
                Source = this,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath(nameof(Value)),

            };

            if (Type == typeof(Color))
            {
                var colorPickerBox = new ColorPickerBox
                {
                    Header = Name,
                    Margin = new Thickness(0, 25, 0, 25)
                };

                colorPickerBox.SetBinding(ColorPickerBox.ColorProperty, bind);
                UiElement = colorPickerBox;
            }
            else if (Type == typeof(float)
                     || Type == typeof(double)
                     || Type == typeof(decimal))
            {
                var numberBox = new NumberBox
                {
                    Header = Name,
                    SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
                    SmallChange = 1,
                    LargeChange = 10,
                    AcceptsExpression = true
                };

                numberBox.SetBinding(NumberBox.ValueProperty, bind);
                UiElement = numberBox;
            }
            else if (Type == typeof(int))
            {
                var numberBox = new NumberBox
                {
                    Header = Name,
                    SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
                    SmallChange = 1,
                    LargeChange = 10,
                    AcceptsExpression = true,
                    NumberFormatter = new DecimalFormatter
                    {
                        FractionDigits = 0
                    }
                };

                numberBox.SetBinding(NumberBox.ValueProperty, bind);
                UiElement = numberBox;
            }

        }
    }
}
