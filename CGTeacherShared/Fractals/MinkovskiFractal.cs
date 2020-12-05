using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Windows.UI;
using CGTeacherShared.Fractals.Abstract;
using CGTeacherShared.Fractals.EventArgs;
using Microsoft.Graphics.Canvas;

namespace CGTeacherShared.Fractals
{
    public class MinkovskiFractal : BaseFractal
    {
        int k = 120;
        int x1 = 50;
        int y1 = 200;
        int iter = 8;
        public override string Name => "MinkovskiFractal";

        public MinkovskiFractal() : base()
        {
            Parameters.AddValue(ParameterNames.LinesColor, typeof(Color), Colors.White);
            Parameters.AddValue(ParameterNames.StartX1, typeof(double));
            Parameters.AddValue(ParameterNames.StartY1, typeof(double));
        }

        public override event EventHandler<RenderStepEventArgs> RenderStep;

        protected override void Render(CanvasDrawingSession canvasDrawingSession, float x, float y, float fractalWidthScale,
            float fractalHeightScale, float width, float height)
        {
            PartialRender(
                canvasDrawingSession,
                (float)Parameters.GetValue<double>(ParameterNames.StartX1),
                (float)Parameters.GetValue<double>(ParameterNames.StartY1)
                );
        }

        void HorizontalMink(CanvasDrawingSession canvasDrawingSession, float x, float y)
        {
            canvasDrawingSession.DrawLine(x, y, x + k / 4, y, Parameters.GetValue<Color>(ParameterNames.LinesColor));

            canvasDrawingSession.DrawLine(x + k / 4, y + k / 4, x + k / 4 + k / 4, y + k / 4, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            canvasDrawingSession.DrawLine( x + k / 4 + k / 4, y - k / 4, x + k - k / 4, y - k / 4, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            canvasDrawingSession.DrawLine( x + k - k / 4, y, x + k, y, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            canvasDrawingSession.DrawLine( x + k / 4, y, x + k / 4, y + k / 4, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            canvasDrawingSession.DrawLine( x + k / 4 + k / 4, y + k / 4, x + k / 4 + k / 4, y - k / 4, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            canvasDrawingSession.DrawLine( x + k - k / 4, y - k / 4, x + k - k / 4, y, Parameters.GetValue<Color>(ParameterNames.LinesColor));
        }

        void VerticalMink(CanvasDrawingSession canvasDrawingSession, float x, float y)
        {
            canvasDrawingSession.DrawLine(x, y, x, y + k / 4, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            canvasDrawingSession.DrawLine(x, y + k / 4, x - k / 4, y + k / 4, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            canvasDrawingSession.DrawLine(x - k / 4, y + k / 4, x - k / 4, y + k / 4 + k / 4, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            canvasDrawingSession.DrawLine( x - k / 4, y + k / 4 + k / 4, x + k / 4, y + k / 4 + k / 4, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            canvasDrawingSession.DrawLine( x + k / 4, y + k / 4 + k / 4, x + k / 4, y + k - k / 4, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            canvasDrawingSession.DrawLine( x + k / 4, y + k - k / 4, x, y + k - k / 4, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            canvasDrawingSession.DrawLine( x, y + k - k / 4, x, y + k, Parameters.GetValue<Color>(ParameterNames.LinesColor));
        }

        void PartialRender(CanvasDrawingSession canvasDrawingSession, float x, float y)
        {
            if (iter == 0)
            {
                canvasDrawingSession.DrawLine( x, y, x + k, y, Parameters.GetValue<Color>(ParameterNames.LinesColor));
            }
            else
            {
                //for (int i = 0; i < iter; i++)
                // {
                HorizontalMink(canvasDrawingSession,x, y);
                VerticalMink(canvasDrawingSession,x + k, y);
                HorizontalMink(canvasDrawingSession,x + k, y + k);
                VerticalMink(canvasDrawingSession,x + 2 * k, y - k);
                VerticalMink(canvasDrawingSession,x + 2 * k, y);
                HorizontalMink(canvasDrawingSession,x + 2 * k, y - k);
                VerticalMink(canvasDrawingSession,x + 3 * k, y - k);
                HorizontalMink(canvasDrawingSession,x + 3 * k, y);
                // }
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
