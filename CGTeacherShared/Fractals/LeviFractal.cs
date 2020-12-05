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
using CGTeacherShared.Fractals.Abstract;
using CGTeacherShared.Fractals.EventArgs;

namespace CGTeacherShared.Fractals
{

    public class LeviFractal : BaseFractal
    {
        public LeviFractal() : base()
        {
            Parameters.AddValue(ParameterNames.LineColors, typeof(Color), Colors.White);
            Parameters.AddValue(ParameterNames.StartX1, typeof(double), 0);
            Parameters.AddValue(ParameterNames.StartX2, typeof(double), 0);
            Parameters.AddValue(ParameterNames.StartY1, typeof(double), 0);
            Parameters.AddValue(ParameterNames.StartY2, typeof(double), 0);
        }

        public override string Name => "LeviFractal";
        public override event EventHandler<RenderStepEventArgs> RenderStep;

        protected override void Render(CanvasDrawingSession canvasDrawingSession, float x, float y, float fractalWidthScale,
            float fractalHeightScale, float width, float height)
        {
            PartialRender(canvasDrawingSession,
                (float)Parameters.GetValue<double>(ParameterNames.StartX1),
                (float)Parameters.GetValue<double>(ParameterNames.StartX2),
                (float)Parameters.GetValue<double>(ParameterNames.StartY1),
                (float)Parameters.GetValue<double>(ParameterNames.StartY2),
                (int)Parameters.GetValue<double>(BaseFractal.ParameterNames.IterationCount));
        }

        private void PartialRender(CanvasDrawingSession Canvas1, float x1, float x2, float y1, float y2, int i)
        {
            if (i == 0)
            {
                Canvas1.DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), Parameters.GetValue<Color>(ParameterNames.LineColors));
            }
            else
            {
                float x3 = (x1 + x2) / 2 + (y2 - y1) / 2;
                float y3 = (y1 + y2) / 2 - (x2 - x1) / 2;
                PartialRender(Canvas1, x1, x3, y1, y3, i - 1);
                PartialRender(Canvas1, x3, x2, y3, y2, i - 1);
            }
        }

        public new static class ParameterNames
        {
            public const string StartX1 = "SX1";
            public const string StartX2 = "SX2";
            public const string StartY1 = "SY1";
            public const string StartY2 = "ІY2";
            public const string LineColors = "LColor";
        }
    }


}