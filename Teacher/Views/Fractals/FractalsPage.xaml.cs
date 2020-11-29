using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
using CGTeacherShared.Fractals;
using Microsoft.Graphics.Canvas.UI.Xaml;


namespace Teacher.Views.Fractals
{
    public sealed partial class FractalsPage : Page
    {
        private CanvasAnimatedDrawEventArgs args;
        LeviFractal fract = new LeviFractal();
        private CanvasRenderTarget canvas;
        public FractalsPage()
        {
            this.InitializeComponent();
            fract.RenderComplete += Fract_RenderComplete;
        }

        private async void Fract_RenderComplete(object sender, CGTeacherShared.Fractals.EventArgs.RenderCompleteEventArgs e)
        {
            canvas = e.RenderTarget;
           await FractalCanvas.Dispatcher.TryRunAsync(CoreDispatcherPriority.Normal, () => FractalCanvas.Invalidate());
        }

        private void Rotate_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            fract.BeginRenderAsync(0, 0, 0, 0, (float)FractalCanvas.ActualWidth, (float)FractalCanvas.ActualHeight);
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

        private void FractalCanvas_OnDraw(CanvasControl sender, CanvasDrawEventArgs canvasDrawEventArgs)
        {
            if (canvas != null)
            {
                canvasDrawEventArgs.DrawingSession.DrawImage(canvas);
            }
        }
    }
}
