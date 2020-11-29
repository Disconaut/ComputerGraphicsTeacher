using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CGTeacherShared.Fractals;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Teacher.Controls;
using Teacher.ViewModels.Fractals;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Teacher.Views.Fractals
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FractalsPage : Page
    {
        public FractalsPageViewModel ViewModel { get; }

        public FractalsPage()
        {
            this.InitializeComponent();
            ViewModel = new FractalsPageViewModel();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ViewModel.CurrentFractal))
                return;

            FillFractalSettings(ViewModel.CurrentFractal.FractalParameters);
        }

        private void FillFractalSettings(IEnumerable<FractalParameterViewModel> parameters)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.Type == typeof(Color))
                {
                    var colorPickerBox = new ColorPickerBox();

                    var bind = new Binding();
                    bind.Source = parameter;
                    bind.Mode = BindingMode.TwoWay;
                    bind.Path = new PropertyPath(nameof(parameter.Value));

                    colorPickerBox.SetBinding(ColorPickerBox.ColorProperty, bind);
                    FractalSettings.Children.Add(colorPickerBox);
                }
            }
        }

        private void Rotate_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ZoomOutBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void MoveLeftBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void MoveUpBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void MoveRightBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void MoveDownBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void FractalCanvas_OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
        }
    }
}
