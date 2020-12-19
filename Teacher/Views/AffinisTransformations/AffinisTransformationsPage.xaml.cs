using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
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
        private const string PointResourceString = "Point";
        private const float CanvasGeometryStrokeWidth = 2;

        public AffinisTransformationsPageViewModel ViewModel { get; }

        public AffinisTransformationsPage()
        {
            ViewModel = new AffinisTransformationsPageViewModel();
            this.InitializeComponent();
            InitPoints();
        }

        private void InitPoints()
        {
            var pointString = ResourceLoader.GetForCurrentView().GetString(PointResourceString);
            pointString = string.IsNullOrEmpty(pointString) ? PointResourceString : pointString;

            var i = 1;
            foreach (var point in ViewModel.Polygon.Points)
            {
                var bind = new Binding
                {
                    Source = point,
                    Mode = BindingMode.TwoWay
                };

                var vectorBox = new VectorBox
                {
                    Header = $"{pointString} {i}"
                };

                vectorBox.SetBinding(VectorBox.VectorProperty, bind);
                Points.Children.Add(vectorBox);

                ++i;
            }
        }

        private void CanvasAnimatedControl_OnDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            args.DrawingSession.Clear(Colors.White);
            var canvasGeometry = ViewModel.DrawShape(args.DrawingSession);
            args.DrawingSession.DrawGeometry(canvasGeometry, Colors.Black, CanvasGeometryStrokeWidth);
        }
    }
}
