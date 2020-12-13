using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;
using CGTeacherShared.AfinnisTransformations;
using CGTeacherShared.Fractals.EventArgs;
using Microsoft.Graphics.Canvas;

namespace CGTeacherShared.Fractals.Abstract
{
    public abstract class BaseFractal : IFractal
    {
        protected BaseFractal()
        {
            Parameters = new FractalParametersSet();
            Parameters.AddValue(ParameterNames.IterationCount, typeof(int), 15.0);
        }

        public abstract string Name { get; }

        public IFractalParametersSet Parameters { get; }

        public event EventHandler<RenderCompleteEventArgs> RenderComplete;

        public Task BeginRenderAsync(Transformation transformation, float width, float height, float dpi, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var device = CanvasDevice.GetSharedDevice();
                var offscreen = new CanvasRenderTarget(device, width, height, dpi);
                using (var ds = offscreen.CreateDrawingSession())
                {
                    ds.Clear(Colors.Black);
                    Render(ds, transformation, width, height, cancellationToken);
                }

                RenderComplete?.Invoke(this, new RenderCompleteEventArgs
                {
                    RenderTarget = offscreen
                });
            }, cancellationToken);
        }

        protected abstract void Render(CanvasDrawingSession canvasDrawingSession, Transformation transformation,
            float width, float height,
            CancellationToken cancellationToken);

        public static class ParameterNames
        {
            public const string IterationCount = "ICount";
        }
    }
}