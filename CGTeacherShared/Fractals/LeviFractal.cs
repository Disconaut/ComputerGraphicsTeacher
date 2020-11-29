using System;
using System.Linq;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using System.Numerics;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace CGTeacherShared.Fractals
{
    public class LeviFractal
    {
        private int i=3;

        void Draw_Levy( int x1, int x2, int y1, int y2, int i, Canvas Canvas1)
        {
            SolidColorBrush brush= new SolidColorBrush(Colors.SeaGreen);
            Line line = new Line();
             line = new Line();
             line.Stroke = brush;
             line.StrokeThickness = 1;
            if (i == 0)
            {
                line.X1 = x1;
                line.X2 = x2;
                line.Y1 = y1;
                line.Y2 = y2;
                double height=Canvas1.ActualHeight;
                double width=Canvas1.ActualWidth;
                
                Canvas1.Children.Add(line);
      
            }
            else
            {
                int x3 = (x1 + x2) / 2 + (y2 - y1) / 2;
                int y3 = (y1 + y2) / 2 - (x2 - x1) / 2;
                Draw_Levy( x1, x3, y1, y3, i - 1, Canvas1);
                Draw_Levy( x3, x2, y3, y2, i - 1, Canvas1);
            }
        }
         public async void DrawKylymok(Canvas Canv)
         {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     Draw_Levy(250, 400, 160, 160, i, Canv);
                 });

                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     Draw_Levy(400, 400, 160, 310, i, Canv);
                 });

                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                   () =>
                   {
                      Draw_Levy(400, 250, 310, 310, i, Canv);
                   });
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                          () =>
                  {
                     Draw_Levy(250, 250, 310, 160, i,Canv);
                  });
               
        }
    }
}