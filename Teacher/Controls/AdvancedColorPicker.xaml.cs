using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.Helpers;
using Teacher.ViewModels.Controls.AdvancedColorPicker;
using ColorHelper = Microsoft.Toolkit.Uwp.Helpers.ColorHelper;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Teacher.Controls
{
    public sealed partial class AdvancedColorPicker : UserControl
    {
        public static readonly DependencyProperty ColorModelsProperty = DependencyProperty.Register(
            "ColorModels", typeof(IEnumerable<ColorModelViewModelBase>), typeof(AdvancedColorPicker), new PropertyMetadata(default(IEnumerable<ColorModelViewModelBase>)));

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

        public string ColorHex
        {
            get => Color.ToHex();
            set => Color = value.ToColor();
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
        }

        private void ColorPicker_OnColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            //Color = args.NewColor;
        }
    }
}
