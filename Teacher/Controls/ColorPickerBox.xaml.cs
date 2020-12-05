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
using ColorPicker = Microsoft.UI.Xaml.Controls.ColorPicker;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Teacher.Controls
{
    public sealed partial class ColorPickerBox : UserControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(string), typeof(ColorPickerBox), new PropertyMetadata(default(string)));

        public string Header
        {
            get => (string) GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color", typeof(Color), typeof(ColorPickerBox), new PropertyMetadata(default(Color)));

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public event EventHandler<Microsoft.UI.Xaml.Controls.ColorChangedEventArgs> ColorChanged; 

        public ColorPickerBox()
        {
            this.InitializeComponent();
        }

        private void ColorPicker_OnColorChanged(ColorPicker sender, Microsoft.UI.Xaml.Controls.ColorChangedEventArgs args)
        {
            ColorChanged?.Invoke(this, args);
        }
    }
}
