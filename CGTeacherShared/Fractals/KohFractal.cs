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
    public class KohFractal:BaseFractal
    {
        public override string Name => "KohFractal";
        public override event EventHandler<RenderStepEventArgs> RenderStep;

        protected override void Render(CanvasDrawingSession canvasDrawingSession, float x, float y, float fractalWidthScale,
            float fractalHeightScale, float width, float height)
        {
            PartialRender(
                canvasDrawingSession,
                Parameters.GetValue<Vector2>(ParameterNames.StartX1).Move(x,y),
                Parameters.GetValue<Vector2>(ParameterNames.StartX2).Move(x, y),
                Parameters.GetValue<Vector2>(ParameterNames.StartY1).Move(x, y),
                Parameters.GetValue<int>(BaseFractal.ParameterNames.IterationCount)
                );
        }

        public KohFractal():base()
        {
            Parameters.AddValue(ParameterNames.StartX1, typeof(float),0);
            Parameters.AddValue(ParameterNames.StartX2, typeof(float),0);
            Parameters.AddValue(ParameterNames.StartY1, typeof(float),0);
            Parameters.AddValue(ParameterNames.StartY2, typeof(float),0);
            Parameters.AddValue(ParameterNames.LinesColor, typeof(Color), Colors.SeaGreen);
        }
        protected void PartialRender(CanvasDrawingSession canvasDrawingSession,Vector2 p1, Vector2 p2, Vector2 p3, int iter)
        {
            if (iter > 0)  
            {
                var p4 = new Vector2((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
                var p5 =  new Vector2((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
                var ps =  new Vector2((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
                var pn =  new Vector2((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3);

                canvasDrawingSession.DrawLine( p4, pn, Parameters.GetValue<Color>(ParameterNames.LinesColor));
                canvasDrawingSession.DrawLine( p5, pn, Parameters.GetValue<Color>(ParameterNames.LinesColor));
                canvasDrawingSession.DrawLine( p4, p5, Parameters.GetValue<Color>(ParameterNames.LinesColor));

                PartialRender(canvasDrawingSession,p4, pn, p5, iter - 1);
                PartialRender(canvasDrawingSession,pn, p5, p4, iter - 1);
                PartialRender(canvasDrawingSession, p1,p4, new  Vector2((2 * p1.X + p3.X) / 3, (2 * p1.Y + p3.Y) / 3), iter - 1);
                PartialRender(canvasDrawingSession, p5, p2, new  Vector2((2 * p2.X + p3.X) / 3, (2 * p2.Y + p3.Y) / 3), iter - 1);
            }
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