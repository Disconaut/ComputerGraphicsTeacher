using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
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

        protected override void Render(CanvasDrawingSession canvasDrawingSession, float x, float y,
            float fractalWidthScale,
            float fractalHeightScale, float width, float height, float angle, CancellationToken cancellationToken)
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
                (int)Parameters.GetValue<double>(BaseFractal.ParameterNames.IterationCount),
                cancellationToken);
        }

        private void PartialRender(CanvasDrawingSession canvasDrawingSession, Vector2 startPoint, Vector2 endPoint,
            int iteration, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (iteration <= 0)
            {
                canvasDrawingSession.DrawLine(startPoint, endPoint, Parameters.GetValue<Color>(ParameterNames.LinesColor));
                return;
            }

            var vector = endPoint - startPoint;
            var vectorPart = vector / 4;
            

            for (int i = 0; i < 4; ++i)
            {
                var firstMiddlePoint = startPoint.Move(vectorPart)
                    .Rotate(90 * (float) Math.Sin(i * Math.PI / 2), startPoint);
                var secondMiddlePoint = endPoint.Move(-vectorPart)
                    .Rotate(90 * (float) Math.Sin(i * Math.PI / 2), endPoint);
                PartialRender(
                    canvasDrawingSession,
                    startPoint,
                    firstMiddlePoint,
                    iteration - 1,
                    cancellationToken);
                PartialRender(
                    canvasDrawingSession,
                    secondMiddlePoint,
                    endPoint,
                    iteration - 1,
                    cancellationToken);

                startPoint = firstMiddlePoint;
                endPoint = secondMiddlePoint;
            }

           

            //startPoint = firstMiddlePoint;
            //firstMiddlePoint = startPoint.Move(vectorPart).Rotate(-90, startPoint);

            //PartialRender(
            //    canvasDrawingSession, 
            //    startPoint,
            //    firstMiddlePoint,
            //    iteration - 1,
            //    cancellationToken);

            //startPoint = firstMiddlePoint;
            //firstMiddlePoint = startPoint.Move(vectorPart);

            //PartialRender(
            //    canvasDrawingSession,
            //    startPoint,
            //    firstMiddlePoint,
            //    iteration - 1,
            //    cancellationToken);

            //startPoint = firstMiddlePoint;
            //firstMiddlePoint = startPoint.Move(vectorPart).Rotate(90, startPoint);

            //PartialRender(
            //    canvasDrawingSession,
            //    startPoint,
            //    firstMiddlePoint,
            //    iteration - 1,
            //    cancellationToken);

            //startPoint = firstMiddlePoint;
            //firstMiddlePoint = startPoint.Move(vectorPart).Rotate(90, startPoint);

            //PartialRender(
            //    canvasDrawingSession,
            //    startPoint,
            //    firstMiddlePoint,
            //    iteration - 1,
            //    cancellationToken);

            //startPoint = firstMiddlePoint;
            //firstMiddlePoint = startPoint.Move(vectorPart);

            //PartialRender(
            //    canvasDrawingSession,
            //    startPoint,
            //    firstMiddlePoint,
            //    iteration - 1,
            //    cancellationToken);

            //startPoint = firstMiddlePoint;
            //firstMiddlePoint = startPoint.Move(vectorPart).Rotate(-90, startPoint);

            //PartialRender(
            //    canvasDrawingSession,
            //    startPoint,
            //    firstMiddlePoint,
            //    iteration - 1,
            //    cancellationToken);

            
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
