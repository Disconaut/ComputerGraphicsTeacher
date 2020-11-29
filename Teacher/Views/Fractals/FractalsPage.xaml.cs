using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CGTeacherShared.Fractals;
using Microsoft.Graphics.Canvas.UI.Xaml;


namespace Teacher.Views.Fractals
{
    public sealed partial class FractalsPage : Page
    {
        private CanvasAnimatedDrawEventArgs args;

        public FractalsPage()
        {
            this.InitializeComponent();
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

            LeviFractal fract = new LeviFractal();
            Thread thread = new Thread(new ThreadStart(() => fract.DrawKylymok(FractalCanvas)));
            thread.Start();
          
          
        }
    }
}
