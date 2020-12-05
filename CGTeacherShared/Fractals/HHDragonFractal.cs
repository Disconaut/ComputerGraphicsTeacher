using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
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
            Parameters.AddValue(ParameterNames.LinesColor, typeof(Color), Colors.White);
            Parameters.AddValue(ParameterNames.StartX1, typeof(double));
            Parameters.AddValue(ParameterNames.StartY1, typeof(double));
            Parameters.AddValue(ParameterNames.StartX2, typeof(double));
            Parameters.AddValue(ParameterNames.StartY2, typeof(double));
            Parameters.AddValue(ParameterNames.StartPoint, typeof(ObservableVector2), new ObservableVector2());
            Parameters.AddValue(ParameterNames.EndPoint, typeof(ObservableVector2), new ObservableVector2());
        }

        public override string Name => "HHDragonFractal";

        public override event EventHandler<RenderStepEventArgs> RenderStep;

        protected override void Render(CanvasDrawingSession canvasDrawingSession, float f, float f1, float fractalWidthScale, float fractalHeightScale, float width, float height)
        {
            PartialRender(
                canvasDrawingSession,
                (Vector2)Parameters.GetValue<ObservableVector2>(ParameterNames.StartPoint),
                (Vector2)Parameters.GetValue<ObservableVector2>(ParameterNames.EndPoint),
                (int)Parameters.GetValue<double>(BaseFractal.ParameterNames.IterationCount));
        }

        private void PartialRender(CanvasDrawingSession canvasDrawingSession, Vector2 startPoint, Vector2 endPoint,
            int iteration)
        {
            if (iteration > 0)
            {
                var middlePoint = new Vector2(
                    (startPoint.X + endPoint.X) / 2 + (endPoint.Y - startPoint.Y) / 2,
                    (startPoint.Y + endPoint.Y) / 2 - (endPoint.X - startPoint.X) / 2);

                PartialRender(canvasDrawingSession, endPoint, middlePoint, iteration - 1);
                PartialRender(canvasDrawingSession, startPoint, middlePoint, iteration - 1);
            }

            canvasDrawingSession.DrawLine(startPoint, endPoint, Parameters.GetValue<Color>(ParameterNames.LinesColor));
        }

        public new static class ParameterNames
        {
            public const string StartPoint = "SP";

            public const string EndPoint = "EP";

            public const string StartX1 = "SX1";

            public const string StartX2 = "SX2";

            public const string StartY1 = "SY1";

            public const string StartY2 = "SY2";

            public const string LinesColor = "LColor";
        } 
    }
}
