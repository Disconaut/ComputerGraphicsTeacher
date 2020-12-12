using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;
using CGTeacherShared.AfinnisTransformations;
using CGTeacherShared.Fractals.Abstract;
using CGTeacherShared.Fractals.EventArgs;
using CGTeacherShared.Shared.Vector;
using Microsoft.Graphics.Canvas;

namespace CGTeacherShared.Fractals
{
    public class KohFractal:BaseFractal
    {
        public override string Name => "KohFractal";
        public KohFractal() : base()
        {
            Parameters.AddValue(ParameterNames.LinesColor, typeof(Color), Colors.Black);
            Parameters.AddValue(ParameterNames.BackgroundColor, typeof(Color), Colors.White);
            Parameters.AddValue(ParameterNames.StartPoint, typeof(ObservableVector2), new ObservableVector2());
            Parameters.AddValue(ParameterNames.EndPoint, typeof(ObservableVector2), new ObservableVector2());
        }
        protected override void Render(CanvasDrawingSession canvasDrawingSession, float x, float y,
            float fractalWidthScale,
            float fractalHeightScale, float width, float height, float angle, CancellationToken cancellationToken)
        {
            canvasDrawingSession.Clear(Parameters.GetValue<Color>(ParameterNames.BackgroundColor));

            var startPoint = (Vector2) Parameters.GetValue<ObservableVector2>(ParameterNames.StartPoint);
            var endPoint = (Vector2) Parameters.GetValue<ObservableVector2>(ParameterNames.EndPoint);

            var centerX = width / 2;
            var centerY = height / 2;
          
            var lineCenter = (startPoint + endPoint) / 2;

            PartialRender(
                canvasDrawingSession,
                startPoint.Rotate(angle, lineCenter).Move(centerX, centerY).Zoom(fractalWidthScale, fractalHeightScale, centerX, centerY).Move(x, y),
                endPoint.Rotate(angle, lineCenter).Move(centerX, centerY).Zoom(fractalWidthScale, fractalHeightScale, centerX, centerY).Move(x, y),
                (int)Parameters.GetValue<double>(BaseFractal.ParameterNames.IterationCount),
                cancellationToken);
        }

        protected void PartialRender(CanvasDrawingSession canvasDrawingSession,Vector2 p1, Vector2 p2, int iter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (iter > 0)
            {
                var L = Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2));
                var h = L / (2 * Math.Sqrt(3));
                var sina = (p2.Y - p1.Y) / L;
                var cosa = (p2.X - p1.X) / L;
                float x = (float)((p2.X + p1.X) / 2 + h * sina);
                float y = (float)((p2.Y + p1.Y) / 2 - h * cosa);

                var pm=new Vector2(x,y);
                
                PartialRender(canvasDrawingSession, pm, p1, iter - 1, cancellationToken);
                PartialRender(canvasDrawingSession, p2, pm, iter - 1, cancellationToken);
            }
            else
            {
              canvasDrawingSession.DrawLine(p1,p2, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            }
        }

        public new static class ParameterNames
        {
            public const string StartPoint = "SP";

            public const string EndPoint = "EP";

            public const string LinesColor = "LColor";

            public const string BackgroundColor = "BColor";
        }
    }
}