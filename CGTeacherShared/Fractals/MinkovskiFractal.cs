using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Windows.UI;
using CGTeacherShared.AfinnisTransformations;
using CGTeacherShared.Fractals.Abstract;
using CGTeacherShared.Fractals.EventArgs;
using CGTeacherShared.Shared.Vector;
using Microsoft.Graphics.Canvas;

namespace CGTeacherShared.Fractals
{
    public class MinkovskiFractal : BaseFractal
    {
        public override string Name => "MinkovskiFractal";

        public MinkovskiFractal() : base()
        {
            Parameters.AddValue(ParameterNames.LinesColor, typeof(Color), Colors.Black);
            Parameters.AddValue(ParameterNames.BackgroundColor, typeof(Color), Colors.White);
            Parameters.AddValue(ParameterNames.StartPoint, typeof(ObservableVector2), new ObservableVector2());
            Parameters.AddValue(ParameterNames.EndPoint, typeof(ObservableVector2), new ObservableVector2());
        }

        protected override void Render(CanvasDrawingSession canvasDrawingSession, float x, float y, float fractalWidthScale,
            float fractalHeightScale, float width, float height, float angle)
        {
            canvasDrawingSession.Clear(Parameters.GetValue<Color>(ParameterNames.BackgroundColor));

            var centerX = width / 2;
            var centerY = height / 2;

            var startPoint = ((Vector2)Parameters.GetValue<ObservableVector2>(ParameterNames.StartPoint));
            var endPoint = ((Vector2)Parameters.GetValue<ObservableVector2>(ParameterNames.EndPoint));

            var lineCenter = (startPoint + endPoint) / 2;

            startPoint = startPoint
                .Rotate(angle, lineCenter)
                .Move(centerX, centerY)
                .Zoom(fractalWidthScale, fractalHeightScale, centerX, centerY)
                .Move(x, y);

            endPoint = endPoint
                .Rotate(angle, lineCenter)
                .Move(centerX, centerY)
                .Zoom(fractalWidthScale, fractalHeightScale, centerX, centerY)
                .Move(x, y);

            PartialRender(
                canvasDrawingSession,
                startPoint,
                endPoint,
                (int)Parameters.GetValue<double>(BaseFractal.ParameterNames.IterationCount));
        }

        private void PartialRender(CanvasDrawingSession canvasDrawingSession, Vector2 startPoint, Vector2 endPoint,
            int iteration)
        {
            if (iteration <= 0)
            {
                canvasDrawingSession.DrawLine(startPoint, endPoint, Parameters.GetValue<Color>(ParameterNames.LinesColor));
                return;
            }

            var vector = endPoint - startPoint;
            var vectorPart = vector / 4;

            PartialRender(
                canvasDrawingSession,
                startPoint, 
                startPoint.Move(vectorPart), 
                iteration - 1);

            startPoint = startPoint.Move(vectorPart);

            PartialRender(
                canvasDrawingSession, 
                startPoint,
                startPoint.Move(vectorPart).Rotate(-90, startPoint),
                iteration - 1);

            startPoint = startPoint.Move(vectorPart).Rotate(-90, startPoint);

            PartialRender(
                canvasDrawingSession,
                startPoint,
                startPoint.Move(vectorPart),
                iteration - 1);

            startPoint = startPoint.Move(vectorPart);

            PartialRender(
                canvasDrawingSession,
                startPoint,
                startPoint.Move(vectorPart).Rotate(90, startPoint),
                iteration - 1);

            startPoint = startPoint.Move(vectorPart).Rotate(90, startPoint);

            PartialRender(
                canvasDrawingSession,
                startPoint,
                startPoint.Move(vectorPart).Rotate(90, startPoint),
                iteration - 1);

            startPoint = startPoint.Move(vectorPart).Rotate(90, startPoint);

            PartialRender(
                canvasDrawingSession,
                startPoint,
                startPoint.Move(vectorPart),
                iteration - 1);

            startPoint = startPoint.Move(vectorPart);

            PartialRender(
                canvasDrawingSession,
                startPoint,
                startPoint.Move(vectorPart).Rotate(-90, startPoint),
                iteration - 1);

            PartialRender(
                canvasDrawingSession, 
                endPoint.Move(-vectorPart),
                endPoint, 
                iteration - 1);
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
