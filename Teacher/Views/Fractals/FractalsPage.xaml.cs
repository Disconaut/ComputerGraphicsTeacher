using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using Microsoft.Toolkit.Uwp.UI.Converters;


namespace Teacher.Views.Fractals
{
    public sealed partial class FractalsPage : Page
    {
        public FractalsPageViewModel ViewModel { get; }
        private readonly Button _drawFractalButton;
        private readonly Button _stopDrawButton;

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

            _stopDrawButton = new Button
            {
                Content = ResourceLoader.GetForCurrentView().GetString("StopDrawButtonContent"),
                Margin = new Thickness(0, 5, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            _stopDrawButton.Click += StopDrawButtonOnClick;

            var bind = new Binding
            {
                Source = ViewModel,
                Path = new PropertyPath(nameof(ViewModel.IsRendering)),
                Converter = Resources["BoolToVisibility"] as BoolToVisibilityConverter
            };

            _stopDrawButton.SetBinding(VisibilityProperty, bind);
        }

        private void StopDrawButtonOnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.CancelRendering();
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
                case nameof(ViewModel.Transformation):
                    UpdateFractal();
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
            FractalSettings.Children.Add(_stopDrawButton);
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
            ViewModel.Transformation.RotateAngle += 45;
        }
        private void RotateMinus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Transformation.RotateAngle -= 45;
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Transformation.WidthScale += ViewModel.Transformation.WidthScale * 0.5f;
            ViewModel.Transformation.HeightScale += ViewModel.Transformation.HeightScale * 0.5f;
        }

        private void ZoomOutBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Transformation.WidthScale -= ViewModel.Transformation.WidthScale * 0.5f;
            ViewModel.Transformation.HeightScale -= ViewModel.Transformation.HeightScale * 0.5f;
        }

        private void MoveLeftBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Transformation.OffsetX += 75;
        }

        private void MoveUpBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Transformation.OffsetY += 75;
        }

        private void MoveRightBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Transformation.OffsetX -= 75;
        }

        private void MoveDownBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Transformation.OffsetY -= 75;
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
            ViewModel.Transformation.WidthScale += (ViewModel.Transformation.WidthScale * (float)Math.Pow(0.05, 2) * wheelDelta);
            ViewModel.Transformation.HeightScale += (ViewModel.Transformation.HeightScale * (float)Math.Pow(0.05, 2) * wheelDelta);
        }

        private async void SaveAs_OnClick(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveFractalToImage();
        }

        private void RestoreBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Transformation.OffsetX = 0;
            ViewModel.Transformation.OffsetY = 0;
            ViewModel.Transformation.WidthScale = 1;
            ViewModel.Transformation.HeightScale = 1;
            ViewModel.Transformation.RotateAngle = 0;
        }
    }
}
