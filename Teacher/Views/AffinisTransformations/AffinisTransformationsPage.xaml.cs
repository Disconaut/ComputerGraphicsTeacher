using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
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
using CGTeacherShared.AfinnisTransformations;
using CGTeacherShared.Shared.Vector;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
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
            ScaleBox.NumberFormatter = new DecimalFormatter
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

            var labelWidth = 10;
            var oxEnd = (new Vector2((float)_canvasActualWidth, 0)).Move(0, (float)_canvasActualHeight / 2);
            var oyEnd = (new Vector2(0, (float)_canvasActualHeight)).Move((float) _canvasActualWidth / 2, 0);

            args.DrawingSession.DrawLine(
                (new Vector2(0, 0)).Move((float)_canvasActualWidth / 2, 0), 
                oyEnd, 
                Colors.Gray, 
                2);
            var pathBuilder = new CanvasPathBuilder(args.DrawingSession);
            pathBuilder.BeginFigure(0, 0);
            var arrowEnd = (new Vector2(0, 0)).Move(5, 5);
            pathBuilder.AddLine(arrowEnd);
            pathBuilder.AddLine(arrowEnd.Move(5, -5));
            pathBuilder.EndFigure(CanvasFigureLoop.Open);
            var downArrow = CanvasGeometry.CreatePath(pathBuilder);
            var downArrowRect = downArrow.ComputeBounds();
            args.DrawingSession.DrawGeometry(downArrow, (float)_canvasActualWidth / 2 - (float)downArrowRect.Width / 2, (float)(_canvasActualHeight - downArrowRect.Height), Colors.Gray, 2);
            var yFormat = new CanvasTextFormat
            {
                FontSize = 14,
                VerticalAlignment = CanvasVerticalAlignment.Bottom,
                HorizontalAlignment = CanvasHorizontalAlignment.Right
            };
            var yLayout = new CanvasTextLayout(args.DrawingSession, "y", yFormat, 50, 20);
            args.DrawingSession.DrawTextLayout(yLayout, (float)_canvasActualWidth / 2 - (float)downArrowRect.Width / 2 - 50 - 2, (float)(_canvasActualHeight - 20), Colors.Gray);

            args.DrawingSession.DrawLine(
                (new Vector2(0, 0)).Move(0, (float)_canvasActualHeight / 2),
                oxEnd,
                Colors.Gray,
                2);

            pathBuilder = new CanvasPathBuilder(args.DrawingSession);
            pathBuilder.BeginFigure(0, 0);
            arrowEnd = (new Vector2(0, 0)).Move(5, 5);
            pathBuilder.AddLine(arrowEnd);
            pathBuilder.AddLine(arrowEnd.Move(-5, 5));
            pathBuilder.EndFigure(CanvasFigureLoop.Open);
            var rightArrow = CanvasGeometry.CreatePath(pathBuilder);
            var rightArrowRect = rightArrow.ComputeBounds();

            args.DrawingSession.DrawGeometry(rightArrow, (float)(_canvasActualWidth - rightArrowRect.Width), (float)_canvasActualHeight / 2 - (float)rightArrowRect.Height / 2, Colors.Gray, 2);
            var xFormat = new CanvasTextFormat
            {
                FontSize = 14,
                VerticalAlignment = CanvasVerticalAlignment.Bottom,
                HorizontalAlignment = CanvasHorizontalAlignment.Right
            };
            var xLayout = new CanvasTextLayout(args.DrawingSession, "x", xFormat, 50, 20);
            args.DrawingSession.DrawTextLayout(xLayout, (float)(_canvasActualWidth - 50), (float)_canvasActualHeight/ 2 - (float)rightArrowRect.Height/ 2 - 20 - 2, Colors.Gray);


            var center = (new Vector2(0, 0)).Move((float)_canvasActualWidth / 2, (float)_canvasActualHeight / 2);

            var countOfXLabels = Math.Ceiling(_canvasActualWidth / 100);
            var countOfYLabels = Math.Ceiling(_canvasActualHeight / 100);

            for (var i = -((int)Math.Ceiling(countOfXLabels / 2) - 1); i <= (int)Math.Ceiling(countOfXLabels / 2) - 1; ++i)
            {
                if(i == 0) continue;
                var moveVector = new Vector2(100, 0) * i;
                var labelPoint = center.Move(moveVector);
                args.DrawingSession.DrawLine(labelPoint.Move(0, (float)labelWidth / 2), labelPoint.Move(0, -(float)labelWidth / 2), Colors.Gray, 1);
                var canvasFontFormat = new CanvasTextFormat
                {
                    FontSize = 10,
                    HorizontalAlignment = CanvasHorizontalAlignment.Center
                };
                var canvasTextLayout = new CanvasTextLayout(args.DrawingSession, moveVector.X.ToString(CultureInfo.InvariantCulture), canvasFontFormat, 50, 20);
                args.DrawingSession.DrawTextLayout(canvasTextLayout, labelPoint.Move(-25, (float)labelWidth / 2 + 2), Colors.Gray);
            }

            for (var i = -((int)Math.Ceiling(countOfYLabels / 2) - 1); i <= (int)Math.Ceiling(countOfYLabels / 2) - 1; ++i)
            {
                if (i == 0) continue;
                var moveVector = new Vector2(0, 100) * i;
                var labelPoint = center.Move(moveVector);
                args.DrawingSession.DrawLine(labelPoint.Move((float)labelWidth / 2, 0), labelPoint.Move(-(float)labelWidth / 2, 0), Colors.Gray, 1);
                var canvasFontFormat = new CanvasTextFormat
                {
                    FontSize = 10,
                    VerticalAlignment = CanvasVerticalAlignment.Center
                };
                var canvasTextLayout = new CanvasTextLayout(args.DrawingSession, moveVector.Y.ToString(CultureInfo.InvariantCulture), canvasFontFormat, 50, 20);
                args.DrawingSession.DrawTextLayout(canvasTextLayout, labelPoint.Move((float)labelWidth / 2 + 2, -10), Colors.Gray);
            }

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
