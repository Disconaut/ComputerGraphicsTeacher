﻿using System;
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
            typeof(AdvancedColorPicker), new PropertyMetadata(default(IEnumerable<ColorModelViewModelBase>)));

        public IEnumerable<ColorModelViewModelBase> ColorModels
        {
            get => (IEnumerable<ColorModelViewModelBase>) GetValue(ColorModelsProperty);
            set => SetValue(ColorModelsProperty, value);
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color", typeof(Color), typeof(AdvancedColorPicker), new PropertyMetadata(Colors.White));

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly DependencyProperty IsHexadecimalVisibleProperty = DependencyProperty.Register(
            "IsHexadecimalVisible", typeof(bool), typeof(AdvancedColorPicker), new PropertyMetadata(true));

        public bool IsHexadecimalVisible
        {
            get => (bool) GetValue(IsHexadecimalVisibleProperty);
            set => SetValue(IsHexadecimalVisibleProperty, value);
        }

        public AdvancedColorPicker()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();

            foreach (var colorModel in ColorModels)
            {
                var bind = new Binding
                {
                    Source = colorModel,
                    Path = new PropertyPath(nameof(colorModel.RgbColor)),
                    Mode = BindingMode.TwoWay
                };

                SetBinding(ColorProperty, bind);
            }
        }

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
                        numBox.Minimum = (double) rangeAttribute.Minimum;
                        numBox.Maximum = (double) rangeAttribute.Maximum;
                    }

                    if (!(element.PropertyType == typeof(float)
                          || element.PropertyType == typeof(double)
                          || element.PropertyType == typeof(decimal)))
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
    }
}
