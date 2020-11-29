using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using CGTeacherShared.Fractals.Abstract;
using CGTeacherShared.Fractals.EventArgs;
using Microsoft.Graphics.Canvas;

namespace CGTeacherShared.Fractals
{
    public class HHDragonFractal: IFractal
    {
        public HHDragonFractal()
        {
            Parameters = new FractalParametersSet();
        }

        public string Name => "HHDragonFractal";
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
                    Render(ds, 100, 500, 600, 500, 35);
                }

                RenderComplete?.Invoke(this, new RenderCompleteEventArgs
                {
                    RenderTarget = offscreen
                });
            });
        }
        

        private void Render(CanvasDrawingSession canv, float x1, float y1, float x2, float y2, int iteration)
        {
            if (iteration > 0)
            {
                var xn = (x1 + x2) / 2 + (y2 - y1) / 2;
                var yn = (y1 + y2) / 2 - (x2 - x1) / 2;

                Render(canv, x2, y2, xn, yn, iteration - 1);
                Render(canv, x1, y1, xn, yn, iteration - 1);
            }

            var point1 = new Vector2(x1, y1);
            var point2 = new Vector2(x2, y2);
            canv.DrawLine(point1, point2, Colors.White);
        }
    }
}
