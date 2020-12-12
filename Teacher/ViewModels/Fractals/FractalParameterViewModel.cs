using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
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
using Windows.UI.Xaml.Controls;
using CGTeacherShared.Shared.Vector;

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
                Path = new PropertyPath(nameof(Value)),
            };

            if (Type == typeof(Color))
            {
                var colorPickerBox = new ColorPickerBox
                {
                    Header = Name,
                    Margin = new Thickness(0, 25, 0, 25)
                };

                colorPickerBox.ColorChanged += (sender, args) =>
                {
                    Value = args.NewColor;
                };

                colorPickerBox.Color = (Color)Value;
                UiElement = colorPickerBox;
            }
            else if (Type == typeof(double) || Type == typeof(int))
            {
                var numberBox = new NumberBox
                {
                    Header = Name,
                    SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
                    SmallChange = 1,
                    LargeChange = 10,
                    AcceptsExpression = true
                };
                if (Type == typeof(int))
                {
                    numberBox.NumberFormatter = new DecimalFormatter
                    {
                        FractionDigits = 0
                    };
                }

                bind.Mode = BindingMode.TwoWay;

                numberBox.SetBinding(NumberBox.ValueProperty, bind);
                UiElement = numberBox;
            }
            else if (Type == typeof(ObservableVector2))
            {
                var vectorBox = new VectorBox
                {
                    Header = Name
                };

                bind.Mode = BindingMode.TwoWay;

                vectorBox.SetBinding(VectorBox.VectorProperty, bind);
                UiElement = vectorBox;
            }
        }
    }
}
