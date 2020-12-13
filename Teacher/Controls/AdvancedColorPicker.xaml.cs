using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.NumberFormatting;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Microsoft.UI.Xaml.Controls;
using Teacher.Attributes;
using Teacher.ViewModels.Controls.AdvancedColorPicker;
using ColorChangedEventArgs = Windows.UI.Xaml.Controls.ColorChangedEventArgs;
using ColorHelper = Microsoft.Toolkit.Uwp.Helpers.ColorHelper;
using ColorPicker = Windows.UI.Xaml.Controls.ColorPicker;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Teacher.Controls
{
    public sealed partial class AdvancedColorPicker : UserControl
    {
        private readonly ResourceLoader _resourceLoader;

        public static readonly DependencyProperty ColorModelsProperty = DependencyProperty.Register(
            "ColorModels", typeof(IEnumerable<ColorModelViewModelBase>),
            typeof(AdvancedColorPicker), new PropertyMetadata(default(IEnumerable<ColorModelViewModelBase>), ColorModelsPropertyChangedCallback));

        private static void ColorModelsPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is AdvancedColorPicker advancedColorPicker)) return;
            if (!(e.NewValue is IEnumerable<ColorModelViewModelBase> colorModels)) return;
            var colorModelViewModelBases = colorModels as ColorModelViewModelBase[] ?? colorModels.ToArray();
            foreach (var colorModel in colorModelViewModelBases)
            {
                colorModel.PropertyChanged += (sender, args) =>
                {
                    foreach (var model in colorModelViewModelBases)
                    {
                        if (model == sender) continue;

                        model.SetRgbColorWithoutNotification(((ColorModelViewModelBase) sender).RgbColor);
                    }
                };
            }
        }

        public IEnumerable<ColorModelViewModelBase> ColorModels
        {
            get => (IEnumerable<ColorModelViewModelBase>)GetValue(ColorModelsProperty);
            set => SetValue(ColorModelsProperty, value);
        }

        public Color Color
        {
            get => (ColorModelComboBox.SelectedItem as ColorModelViewModelBase)?.RgbColor ?? Colors.White;
            set
            {
                if (ColorModelComboBox.SelectedItem is ColorModelViewModelBase colorModel)
                {
                    colorModel.RgbColor = value;
                }
            }
        }

        public static readonly DependencyProperty IsHexadecimalVisibleProperty = DependencyProperty.Register(
            "IsHexadecimalVisible", typeof(bool), typeof(AdvancedColorPicker), new PropertyMetadata(true));

        public bool IsHexadecimalVisible
        {
            get => (bool)GetValue(IsHexadecimalVisibleProperty);
            set => SetValue(IsHexadecimalVisibleProperty, value);
        }

        public AdvancedColorPicker()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
        }

        public event EventHandler<ColorChangedEventArgs> ColorChanged; 

        private void ColorModelComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BuildSettingsPanel(ColorModelComboBox.SelectedItem as ColorModelViewModelBase);
        }

        private void BuildSettingsPanel(ColorModelViewModelBase colorModel)
        {
            SettingsPanel.Children.Clear();
            var uiElements = colorModel
                .GetType()
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(UIElementAttribute)));

            foreach (var element in uiElements)
            {
                var attributes = element.GetCustomAttributes();
                var enumerable = attributes as Attribute[] ?? attributes.ToArray();
                var uiElementAttribute = enumerable.OfType<UIElementAttribute>().First();

                var stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                var bind = new Binding
                {
                    Source = colorModel,
                    Path = new PropertyPath(element.Name),
                    Mode = BindingMode.TwoWay
                };

                if (uiElementAttribute.ElementType == typeof(byte)
                    || uiElementAttribute.ElementType == typeof(short)
                    || uiElementAttribute.ElementType == typeof(int)
                    || uiElementAttribute.ElementType == typeof(long)
                    || uiElementAttribute.ElementType == typeof(float)
                    || uiElementAttribute.ElementType == typeof(double)
                    || uiElementAttribute.ElementType == typeof(decimal))
                {
                    var numBox = new NumberBox
                    {
                        Width = 120,
                        SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
                        Margin = new Thickness(0, 6, 12, 6)
                    };

                    var rangeAttribute = enumerable.OfType<RangeAttribute>().FirstOrDefault();
                    if (rangeAttribute != null)
                    {
                        numBox.Minimum = (double)rangeAttribute.Minimum;
                        numBox.Maximum = (double)rangeAttribute.Maximum;
                    }

                    if (!(uiElementAttribute.ElementType == typeof(float)
                          || uiElementAttribute.ElementType == typeof(double)
                          || uiElementAttribute.ElementType == typeof(decimal)))
                    {
                        numBox.NumberFormatter = new DecimalFormatter
                        {
                            FractionDigits = 0
                        };
                    }

                    numBox.SetBinding(NumberBox.ValueProperty, bind);

                    stackPanel.Children.Add(numBox);
                }

                var name = _resourceLoader.GetString(uiElementAttribute.Name);
                name = string.IsNullOrEmpty(name) ? uiElementAttribute.Name : name;

                stackPanel.Children.Add(new TextBlock
                {
                    Text = name,
                    VerticalAlignment = VerticalAlignment.Center
                });

                SettingsPanel.Children.Add(stackPanel);
            }
        }

        private void ColorPicker_OnColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            ColorChanged?.Invoke(this, args);
        }
    }
}
