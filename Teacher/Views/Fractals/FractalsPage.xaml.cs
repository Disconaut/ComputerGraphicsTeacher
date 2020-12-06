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
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using CGTeacherShared.Fractals;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Teacher.Controls;
using Teacher.ViewModels.Fractals;
using CGTeacherShared.Fractals;
using Microsoft.Graphics.Canvas.UI.Xaml;


namespace Teacher.Views.Fractals
{
    public sealed partial class FractalsPage : Page
    {
        public FractalsPageViewModel ViewModel { get; }
        private readonly Button _drawFractalButton;

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
            FractalSettings.Children.Clear();

            FractalSettings.Children.Add(FractalTypeComboBox);

            foreach (var parameter in parameters)
            {
                FractalSettings.Children.Add(parameter.UiElement);
            }

            FractalSettings.Children.Add(_drawFractalButton);
        }

        private void DrawFractalButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateFractal();
        }

        private void UpdateFractal()
        {
            ViewModel.RenderCurrentFractal((float)FractalCanvas.ActualWidth, (float)FractalCanvas.ActualHeight);
        }

        private void RotatePlus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RotateAngle += 45;
            UpdateFractal();
        }
        private void RotateMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RotateAngle -= 45;
            UpdateFractal();
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.WidthScale += 0.5f;
            ViewModel.HeightScale += 0.5f;
            UpdateFractal();
        }

        private void ZoomOutBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.WidthScale -= 0.5f;
            ViewModel.HeightScale -= 0.5f;
            UpdateFractal();
        }

        private void MoveLeftBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.OffsetX -= 75;
             UpdateFractal();
        }

        private void MoveUpBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.OffsetY -= 75;
            UpdateFractal();
        }

        private void MoveRightBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.OffsetX += 75;
            UpdateFractal();
        }

        private void MoveDownBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.OffsetY += 75;
            UpdateFractal();
        }

        private void FractalCanvas_OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (ViewModel.RenderTarget != null)
            {
                args.DrawingSession.DrawImage(ViewModel.RenderTarget);
            }
        }

        private void FractalCanvas_OnPointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            var wheelDelta = e.GetCurrentPoint(sender as UIElement).Properties.MouseWheelDelta;
            ViewModel.WidthScale += (0.1f * wheelDelta);
            ViewModel.HeightScale += (0.1f * wheelDelta);
            UpdateFractal();
        }

        private async void SaveAs_OnClick(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveFractalToImage();
        }
    }
}
