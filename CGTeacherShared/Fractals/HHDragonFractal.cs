﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using CGTeacherShared.Fractals.Abstract;
using CGTeacherShared.Fractals.EventArgs;
using Microsoft.Graphics.Canvas;

namespace CGTeacherShared.Fractals
{
    public class HHDragonFractal: BaseFractal
    {
        public HHDragonFractal(): base()
        {
            Parameters.AddValue(ParameterNames.StartX1, typeof(float));
            Parameters.AddValue(ParameterNames.StartX2, typeof(float));
            Parameters.AddValue(ParameterNames.StartY1, typeof(float));
            Parameters.AddValue(ParameterNames.StartY2, typeof(float));
            Parameters.AddValue(ParameterNames.LinesColor, typeof(Color));
            Parameters.SetValue(ParameterNames.LinesColor, Colors.White);
        }

        public override string Name => "HHDragonFractal";

        public override event EventHandler<RenderStepEventArgs> RenderStep;

        protected override void Render(CanvasDrawingSession canvasDrawingSession, float f, float f1, float fractalWidthScale, float fractalHeightScale, float width, float height)
        {
            PartialRender(
                canvasDrawingSession,
                Parameters.GetValue<float>(ParameterNames.StartX1),
                Parameters.GetValue<float>(ParameterNames.StartY1),
                Parameters.GetValue<float>(ParameterNames.StartX2),
                Parameters.GetValue<float>(ParameterNames.StartY2),
                Parameters.GetValue<int>(BaseFractal.ParameterNames.IterationCount));
        }

        private void PartialRender(CanvasDrawingSession canvasDrawingSession, float x1, float y1, float x2, float y2,
            int iteration)
        {
            if (iteration > 0)
            {
                var xn = (x1 + x2) / 2 + (y2 - y1) / 2;
                var yn = (y1 + y2) / 2 - (x2 - x1) / 2;

                PartialRender(canvasDrawingSession, x2, y2, xn, yn, iteration - 1);
                PartialRender(canvasDrawingSession, x1, y1, xn, yn, iteration - 1);
            }

            var point1 = new Vector2(x1, y1);
            var point2 = new Vector2(x2, y2);
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