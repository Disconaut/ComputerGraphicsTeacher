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

        protected override void Render(CanvasDrawingSession canvasDrawingSession, float x, float y, float fractalWidthScale,
            float fractalHeightScale, float width, float height, float engel)
        {
            canvasDrawingSession.Clear(Parameters.GetValue<Color>(ParameterNames.BackgroundColor));
            PartialRender(
                canvasDrawingSession,
                (Vector2)Parameters.GetValue<ObservableVector2>(ParameterNames.StartPoint),
                (Vector2)Parameters.GetValue<ObservableVector2>(ParameterNames.EndPoint),
                (int)Parameters.GetValue<double>(BaseFractal.ParameterNames.IterationCount));
        }

        private void PartialRender(CanvasDrawingSession Canvas1, Vector2 startPoint, Vector2 endPoint, int iteration)
        {
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
                    iteration - 1);
                PartialRender(Canvas1,
                    middlePoint,
                    endPoint,
                    iteration - 1);
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