using System;
using System.Linq;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using CGTeacherShared.Fractals.Abstract;
using CGTeacherShared.Fractals.EventArgs;

namespace CGTeacherShared.Fractals
{

    public class LeviFractal : IFractal
    {
        public LeviFractal()
        {
            Parameters = new FractalParametersSet();
        }

        public string Name => "LeviFractal";
        public IFractalParametersSet Parameters { get; }
        public event EventHandler<RenderCompleteEventArgs> RenderComplete;

        public Task BeginRenderAsync(float x, float y, float fractalWidth, float fractalHeight, float width,
            float height)
        {
            return Task.Run(() =>
            {
                CanvasDevice device = CanvasDevice.GetSharedDevice();
                CanvasRenderTarget offscreen = new CanvasRenderTarget(device, width, height, 96);
                using (CanvasDrawingSession ds = offscreen.CreateDrawingSession())
                {
                    ds.Clear(Colors.Black);
                    Render(ds, 250, 400, 160, 160, 20);
                }

                RenderComplete?.Invoke(this, new RenderCompleteEventArgs
                {
                    RenderTarget = offscreen
                });
            });
        }


        private void Render(CanvasDrawingSession Canvas1, float x1, float x2, float y1, float y2, int i)
        {
          
                if (i == 0)
                {
                    Canvas1.DrawLine(new Vector2(x1,y1), new Vector2(x2,y2), Colors.SeaGreen);
                }
                else
                {
                    float x3 = (x1 + x2) / 2 + (y2 - y1) / 2;
                    float y3 = (y1 + y2) / 2 - (x2 - x1) / 2;
                    Render(Canvas1, x1, x3, y1, y3, i - 1);
                    Render(Canvas1,x3, x2, y3, y2, i - 1);
                }
        }
    }
}