using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.NumberFormatting;
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
using Microsoft.UI.Xaml.Controls;
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
        private Button _drawFractalButton;

        public FractalsPage()
        {
            this.InitializeComponent();
            ViewModel = new FractalsPageViewModel();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            _drawFractalButton = new Button
            {
                Content = ResourceLoader.GetForCurrentView().GetString("DrawFractalButtonContent"),
                Margin = new Thickness(0, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Style = Resources["AccentButtonStyle"] as Style
            };

            _drawFractalButton.Click += DrawFractalButton_Click;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.CurrentFractal):
                    FillFractalSettings(ViewModel.CurrentFractal.FractalParameters);
                    break;
                case nameof(ViewModel.RenderTarget):
                    FractalCanvas.Invalidate();
                    break;
            }
        }

        private void FillFractalSettings(IEnumerable<FractalParameterViewModel> parameters)
        {
            foreach (var fractalSettingsChild in FractalSettings.Children)
            {
                if (fractalSettingsChild != FractalTypeComboBox)
                {
                    FractalSettings.Children.Remove(fractalSettingsChild);
                }
            }

            foreach (var parameter in parameters)
            {
                FractalSettings.Children.Add(parameter.UiElement);
            }

            FractalSettings.Children.Add(_drawFractalButton);
        }

        private void DrawFractalButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RenderCurrentFractal((float)FractalCanvas.ActualWidth, (float)FractalCanvas.ActualHeight);
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
            if (ViewModel.RenderTarget != null)
            {
                args.DrawingSession.DrawImage(ViewModel.RenderTarget);
            }
        }
    }
}
