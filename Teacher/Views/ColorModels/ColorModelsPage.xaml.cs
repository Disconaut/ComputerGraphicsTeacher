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
using Windows.UI.Xaml.Navigation;
using Teacher.ViewModels.Controls.AdvancedColorPicker;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Teacher.Views.ColorModels
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorModelsPage : Page
    {
        public ObservableCollection<ColorModelViewModelBase> Mod { get; set; }

        public ColorModelsPage()
        {
            Mod = new ObservableCollection<ColorModelViewModelBase>
            {
                new RgbViewModel(Colors.White)
            };
            this.InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
