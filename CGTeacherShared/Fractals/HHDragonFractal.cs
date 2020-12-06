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
    public class HHDragonFractal: BaseFractal
    {
        public HHDragonFractal() : base()
        {
            Parameters.AddValue(ParameterNames.LinesColor, typeof(Color), Colors.Black);
            Parameters.AddValue(ParameterNames.BackgroundColor, typeof(Color), Colors.White);
            Parameters.AddValue(ParameterNames.StartPoint, typeof(ObservableVector2), new ObservableVector2());
            Parameters.AddValue(ParameterNames.EndPoint, typeof(ObservableVector2), new ObservableVector2());
        }

        public override string Name => "HHDragonFractal";

       
        protected override void Render(CanvasDrawingSession canvasDrawingSession, float x, float y,
            float fractalWidthScale, float fractalHeightScale, float width, float height, float angle,
            CancellationToken cancellationToken)
        {
            canvasDrawingSession.Clear(Parameters.GetValue<Color>(ParameterNames.BackgroundColor));

            var point1 = (Vector2) Parameters.GetValue<ObservableVector2>(ParameterNames.StartPoint);
            var point2 = (Vector2) Parameters.GetValue<ObservableVector2>(ParameterNames.EndPoint);

            var centerX = width / 2;
            var centerY = height / 2;

            var lineCenter = (point1 + point2) / 2;

            PartialRender(
                canvasDrawingSession,
                point1.Rotate(angle, lineCenter).Move(centerX, centerY).Zoom(fractalWidthScale, fractalHeightScale, centerX, centerY).Move(x, y),
                point2.Rotate(angle, lineCenter).Move(centerX, centerY).Zoom(fractalWidthScale, fractalHeightScale, centerX, centerY).Move(x, y),
                (int) Parameters.GetValue<double>(BaseFractal.ParameterNames.IterationCount),
                cancellationToken);
        }

        private void PartialRender(CanvasDrawingSession canvasDrawingSession, Vector2 startPoint, Vector2 endPoint, int iteration, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (iteration > 0)
            {
                var middlePoint = new Vector2(
                    (startPoint.X + endPoint.X) / 2 + (endPoint.Y - startPoint.Y) / 2,
                    (startPoint.Y + endPoint.Y) / 2 - (endPoint.X - startPoint.X) / 2);

                PartialRender(canvasDrawingSession, endPoint, middlePoint, iteration - 1, cancellationToken);
                PartialRender(canvasDrawingSession, startPoint, middlePoint, iteration - 1, cancellationToken);
            }
            canvasDrawingSession.DrawLine(startPoint, endPoint, Parameters.GetValue<Color>(ParameterNames.LinesColor));
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
