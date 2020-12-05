using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using CGTeacherShared.AfinnisTransformations;
using CGTeacherShared.Fractals.Abstract;
using CGTeacherShared.Fractals.EventArgs;
using Microsoft.Graphics.Canvas;

namespace CGTeacherShared.Fractals
{
    public class HHDragonFractal: BaseFractal
    {
        public HHDragonFractal() : base()
        {
            Parameters.AddValue(ParameterNames.LinesColor, typeof(Color), Colors.White);
            Parameters.AddValue(ParameterNames.StartX1, typeof(double), 0);
            Parameters.AddValue(ParameterNames.StartY1, typeof(double), 0);
            Parameters.AddValue(ParameterNames.StartX2, typeof(double), 0);
            Parameters.AddValue(ParameterNames.StartY2, typeof(double), 0);
        }

        public override string Name => "HHDragonFractal";

        public override event EventHandler<RenderStepEventArgs> RenderStep;

        protected override void Render(CanvasDrawingSession canvasDrawingSession, float f, float f1, float fractalWidthScale, float fractalHeightScale, float width, float height)
        {
            var point1 = new Vector2((float)Parameters.GetValue<double>(ParameterNames.StartX1),
                (float)Parameters.GetValue<double>(ParameterNames.StartY1));
            var point2 = new Vector2((float) Parameters.GetValue<double>(ParameterNames.StartX2),
                (float) Parameters.GetValue<double>(ParameterNames.StartY2));

            PartialRender(
                canvasDrawingSession,
                point1.Move(f, f1),
                point2.Move(f,f1),
                (int)Parameters.GetValue<double>(BaseFractal.ParameterNames.IterationCount));
        }
  
        private void PartialRender(CanvasDrawingSession canvasDrawingSession, Vector2 point1, Vector2 point2,
            int iteration)
        {
            var x1 = point1.X;
            var y1 = point1.Y;
            var x2 = point2.X;
            var y2 = point2.Y;
            if (iteration > 0)
            {
                var xn = (x1 + x2) / 2 + (y2 - y1) / 2;
                var yn = (y1 + y2) / 2 - (x2 - x1) / 2;
               
                PartialRender(canvasDrawingSession, new Vector2(x2, y2), new Vector2(xn, yn), iteration - 1);
                PartialRender(canvasDrawingSession, new Vector2(x1, y1), new Vector2(xn, yn), iteration - 1);
            }
            canvasDrawingSession.DrawLine(point1, point2, Parameters.GetValue<Color>(ParameterNames.LinesColor));
        }

        public new static class ParameterNames
        {
            public const string StartX1 = "SX1";

            public const string StartX2 = "SX2";

            public const string StartY1 = "SY1";

            public const string StartY2 = "SY2";

            public const string LinesColor = "LColor";
        } 
    }
}
