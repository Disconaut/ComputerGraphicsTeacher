using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
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
using CGTeacherShared.Shared.Vector;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Teacher.Controls;
using Teacher.ViewModels.AffinisTransformations;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Teacher.Views.AffinisTransformations
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AffinisTransformationsPage : Page
    {
        private const float CanvasGeometryStrokeWidth = 2;

        private readonly string _pointString;
        private double _canvasActualWidth;
        private double _canvasActualHeight;

        public AffinisTransformationsPageViewModel ViewModel { get; }

        public AffinisTransformationsPage()
        {
            _pointString = ResourceLoader.GetForCurrentView().GetString("Point");
            _pointString = string.IsNullOrEmpty(_pointString) ? "Point" : _pointString;
            ViewModel = new AffinisTransformationsPageViewModel();
            this.InitializeComponent();
            Scale.NumberFormatter = new DecimalFormatter
            {
                FractionDigits = 3
            };

            InitPoints();
            RotatePoints.SelectedIndex = 0;
            ShapeCanvas.RenderTransformOrigin = new Point(ShapeCanvas.ActualWidth / 2, ShapeCanvas.ActualHeight / 2);
        }

        private void InitPoints()
        {
            var i = 1;
            foreach (var point in ViewModel.Polygon.Points)
            {
                var bind = new Binding
                {
                    Source = point,
                    Mode = BindingMode.TwoWay
                };

                var header = $"{_pointString} {i}";

                var vectorBox = new VectorBox
                {
                    Header = header
                };

                vectorBox.SetBinding(VectorBox.VectorProperty, bind);
                Points.Children.Add(vectorBox);
                var radioButton = new RadioButton
                {
                    Content = header,
                    Tag = point
                };

                RotatePoints.Items.Add(radioButton);

                ++i;
            }
        }

        private void CanvasAnimatedControl_OnDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var s = sender as CanvasAnimatedControl;
            if(s == null) return;

            args.DrawingSession.Clear(Colors.White);
            var canvasGeometry = ViewModel.DrawShape(args.DrawingSession);
            args.DrawingSession.DrawGeometry(canvasGeometry, (float)_canvasActualWidth / 2, (float)_canvasActualHeight / 2, Colors.Black, CanvasGeometryStrokeWidth);
        }

        private void RotatePoints_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if (!((RotatePoints.SelectedItem as RadioButton)?.Tag is ObservableVector2 selectedPoint)) return;

           ViewModel.Transformation.CenterOfRotation = selectedPoint;
        }

        private void AffinisTransformationsPage_OnLayoutUpdated(object sender, object e)
        {
            _canvasActualWidth = ShapeCanvas.ActualWidth;
            _canvasActualHeight = ShapeCanvas.ActualHeight;
        }
    }
}
