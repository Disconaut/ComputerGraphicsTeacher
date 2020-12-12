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
using CGTeacherShared.AfinnisTransformations;
using CGTeacherShared.Fractals.Abstract;
using CGTeacherShared.Fractals.EventArgs;
using CGTeacherShared.Shared.Vector;

namespace CGTeacherShared.Fractals
{

    public class LeviFractal : BaseFractal
    {
        public LeviFractal() : base()
        {
            Parameters.AddValue(ParameterNames.LineColors, typeof(Color), Colors.Black);
            Parameters.AddValue(ParameterNames.BackgroundColor, typeof(Color), Colors.White);
            Parameters.AddValue(ParameterNames.StartPoint, typeof(ObservableVector2), new ObservableVector2());
            Parameters.AddValue(ParameterNames.EndPoint, typeof(ObservableVector2), new ObservableVector2());
        }

        public override string Name => "LeviFractal";

        protected override void Render(CanvasDrawingSession canvasDrawingSession, Transformation transformation, float width, float height, CancellationToken cancellationToken)
        {
            canvasDrawingSession.Clear(Parameters.GetValue<Color>(ParameterNames.BackgroundColor));

            var centerX = width / 2;
            var centerY = height / 2;

            var startPoint = ((Vector2) Parameters.GetValue<ObservableVector2>(ParameterNames.StartPoint));
            var endPoint = ((Vector2) Parameters.GetValue<ObservableVector2>(ParameterNames.EndPoint));

            var lineCenter = (startPoint + endPoint) / 2;

            startPoint = startPoint
                .Rotate(transformation.RotateAngle, lineCenter)
                .Move(centerX, centerY)
                .Zoom(transformation.WidthScale, transformation.HeightScale, centerX, centerY)
                .Move(transformation.OffsetX, transformation.OffsetY);

            endPoint = endPoint
                .Rotate(transformation.RotateAngle, lineCenter)
                .Move(centerX, centerY)
                .Zoom(transformation.WidthScale, transformation.HeightScale, centerX, centerY)
                .Move(transformation.OffsetX, transformation.OffsetY);

            PartialRender(
                canvasDrawingSession,
                startPoint,
                endPoint,
                (int)Parameters.GetValue<double>(BaseFractal.ParameterNames.IterationCount),
                cancellationToken);
        }

        private void PartialRender(CanvasDrawingSession Canvas1, Vector2 startPoint, Vector2 endPoint, int iteration, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (iteration == 0)
            {
                Canvas1.DrawLine(startPoint, endPoint, Parameters.GetValue<Color>(ParameterNames.LineColors));
            }
            else
            {
                var middlePoint = new Vector2(
                    (startPoint.X + endPoint.X) / 2 + (endPoint.Y - startPoint.Y) / 2,
                    (startPoint.Y + endPoint.Y) / 2 - (endPoint.X - startPoint.X) / 2);

                PartialRender(
                    Canvas1,
                    startPoint,
                    middlePoint,
                    iteration - 1,
                    cancellationToken);
                PartialRender(Canvas1,
                    middlePoint,
                    endPoint,
                    iteration - 1,
                    cancellationToken);
            }
        }

        public new static class ParameterNames
        {
            public const string StartPoint = "SP";
            public const string EndPoint = "EP";
            public const string LineColors = "LColor";
            public const string BackgroundColor = "BColor";
        }
    }


}