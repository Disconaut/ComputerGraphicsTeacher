using System;
using System.Linq;
using System.Numerics;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using CGTeacherShared.Shared.Vector;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;

namespace CGTeacherShared.AfinnisTransformations
{
    public class Trapeze : IPolygon
    {
        public Trapeze()
        {
            Points = new[]
            {
                new ObservableVector2(),
                new ObservableVector2(),
                new ObservableVector2(),
                new ObservableVector2()
            };
        }

        public IShape Transform(Transformation transformation)
        {
            var newTrapeze = new Trapeze();
            var avgX = 0.0;
            var avgY = 0.0;

            foreach (var point in Points)
            {
                avgX += point.X ?? 0;
                avgY += point.Y ?? 0;
            }

            avgX /= Points.Length;
            avgY /= Points.Length;

            for (var i = 0; i < Points.Length; ++i)
            {
                var transformedPoint = ((Vector2)Points[i])
                    .Rotate(
                        transformation.RotateAngle,
                        transformation.XCenterOfRotation,
                        transformation.YCenterOfRotation)
                    .Zoom(transformation.WidthScale,
                        transformation.HeightScale,
                        (float)avgX,
                        (float)avgY);
                newTrapeze.Points[i] = new ObservableVector2(transformedPoint.X, transformedPoint.Y);
            }

            return newTrapeze;
        }

        public CanvasGeometry DrawShape(ICanvasResourceCreator canvasResourceCreator)
        {
            return CanvasGeometry.CreatePolygon(canvasResourceCreator, Points.Select(x => (Vector2)x).ToArray());
        }

        public ObservableVector2[] Points { get; }
    }
}
