using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Teacher.ViewModels.ColorModels;
using Teacher.ViewModels.Controls.AdvancedColorPicker;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Teacher.Views.ColorModels
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorModelsPage : Page
    {
        public ColorModelPageViewModel ViewModel { get; private set; }

        public ColorModelsPage()
        {
            this.InitializeComponent();
            ViewModel = new ColorModelPageViewModel();
        }

        private async void UploadButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearAll();
            await ViewModel.OpenImage();
        }

        private void EyedropperToolButton_OnPickCompleted(EyedropperToolButton sender, EventArgs args)
        {
            ViewModel.ClearChose();
            ColorPicker.Color = sender.Color;
            ViewModel.ChoosePixelWithColor(sender.Color, Image.CroppedRegion);
        }

        private void AdvancedColorPicker_OnColorChanged(object sender, ColorChangedEventArgs e)
        {
            ViewModel.ChangeColorOfChosePixels(e.NewColor);
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveImage();
        }

        private void RevertButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.RevertChanges();
        }

        private void BrightnessRange_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            ViewModel.AdjustBrightness((int)e.NewValue, Image.CroppedRegion);
        }
    }
}
