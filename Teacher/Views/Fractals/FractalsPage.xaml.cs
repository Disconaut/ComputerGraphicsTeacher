using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Teacher.Views.Fractals
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FractalsPage : Page
    {
        private CanvasRenderTarget t;
        private HHDragonFractal frac = new HHDragonFractal();

        public FractalsPage()
        {
            this.InitializeComponent();
            frac.RenderComplete += async (sender, args) =>
            {
                 t = args.RenderTarget;
                await FractalCanvas.Dispatcher.TryRunAsync(CoreDispatcherPriority.Normal, () => FractalCanvas.Invalidate());
            };
            
        }

        private void Rotate_Click(object sender, RoutedEventArgs e)
        {
            frac.BeginRenderAsync(0, 0, 0, 0, (float) FractalCanvas.ActualWidth, (float) FractalCanvas.ActualHeight);
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
            if(t != null)
                args.DrawingSession.DrawImage(t);
        }
    }
}
