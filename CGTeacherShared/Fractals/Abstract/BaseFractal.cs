using System;
using System.Threading.Tasks;
using Windows.UI;
using CGTeacherShared.Fractals.EventArgs;
using Microsoft.Graphics.Canvas;

namespace CGTeacherShared.Fractals.Abstract
{
    public abstract class BaseFractal : IFractal
    {
        protected BaseFractal()
        {
            Parameters = new FractalParametersSet();
            Parameters.AddValue(ParameterNames.IterationCount, typeof(int));
        }

        public abstract string Name { get; }

        public IFractalParametersSet Parameters { get; }

        public event EventHandler<RenderCompleteEventArgs> RenderComplete;
        public abstract event EventHandler<RenderStepEventArgs> RenderStep;

        public Task BeginRenderAsync(float x, float y, float fractalWidthScale, float fractalHeightScale, float width, float height, float dpi)
        {
            return Task.Run(() =>
            {
                var device = CanvasDevice.GetSharedDevice();
                var offscreen = new CanvasRenderTarget(device, width, height, dpi);
                using (var ds = offscreen.CreateDrawingSession())
                {
                    ds.Clear(Colors.Black);
                    Render(ds, x, y, fractalWidthScale, fractalHeightScale, width, height);
                }

                RenderComplete?.Invoke(this, new RenderCompleteEventArgs
                {
                    RenderTarget = offscreen
                });
            });
        }

        protected abstract void Render(CanvasDrawingSession canvasDrawingSession, float x, float y, float fractalWidthScale, float fractalHeightScale, float width, float height);

        public static class ParameterNames
        {
            public const string IterationCount = "ICount";
        }
    }
}