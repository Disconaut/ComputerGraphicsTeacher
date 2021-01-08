using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using CGTeacherShared.Shared.Vector;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;

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

        public Trapeze(IEnumerable<ObservableVector2> points)
        {
            Points = points.ToArray();
        }

        public IShape Transform(Transformation transformation)
        {
            var rotateRads = Math.PI * transformation.RotateAngle / 180;

            //var rotateTransformMatrix = new[]
            //{
            //    new[]
            //    {
            //        (float) Math.Cos(rotateRads),
            //        (float) -Math.Sin(rotateRads),
            //        0
            //    },
            //    new[]
            //    {
            //        (float) Math.Sin(rotateRads),
            //        (float) Math.Cos(rotateRads),
            //        0
            //    },
            //    new[]
            //    {
            //        (float) ((transformation.CenterOfRotation.X ?? 0) -
            //                 (transformation.CenterOfRotation.X ?? 0) * Math.Cos(rotateRads) -
            //                 (transformation.CenterOfRotation.Y ?? 0) * Math.Sin(rotateRads)),
            //        (float) ((transformation.CenterOfRotation.Y ?? 0) -
            //                 (transformation.CenterOfRotation.Y ?? 0) * Math.Cos(rotateRads) +
            //                 (transformation.CenterOfRotation.X ?? 0) * Math.Sin(rotateRads)),
            //        0
            //    }
            //};

            //var rotatedPoints = Points.Transform(rotateTransformMatrix);

            var avgX = 0.0;
            var avgY = 0.0;

            foreach (var point in Points)
            {
                avgX += point.X ?? 0;
                avgY += point.Y ?? 0;
            }

            avgX /= Points.Length;
            avgY /= Points.Length;

            //var zoomTransformMatrix = new[]
            //{
            //    new[]
            //    {
            //        transformation.WidthScale,
            //        0,
            //        0
            //    },
            //    new[]
            //    {
            //        0,
            //        transformation.HeightScale,
            //        0
            //    },
            //    new[]
            //    {
            //        (float)(-transformation.WidthScale * avgX + avgX),
            //        (float)(-transformation.HeightScale * avgY + avgY),
            //        1
            //    }
            //};

            var centerOfRotation = new[]
            {
                transformation.CenterOfRotation
            }.Transform(new[]
            {
                new[] {transformation.WidthScale, 0, 0},
                new[] {0, transformation.HeightScale, 0},
                new[] {(float)(-transformation.WidthScale * avgX + avgX), (float)(-transformation.HeightScale * avgY + avgY), 1}
            }).FirstOrDefault();

            var transformMatrix = new[]
            {
                new[]
                {
                    transformation.WidthScale * (float) Math.Cos(rotateRads),
                    -transformation.WidthScale * (float) Math.Sin(rotateRads),
                    0
                },
                new[]
                {
                    transformation.HeightScale * (float) Math.Sin(rotateRads),
                    transformation.HeightScale * (float) Math.Cos(rotateRads),
                    0
                },
                new[]
                {
                    (float) ((centerOfRotation?.X ?? 0) -
                        (centerOfRotation?.X ?? 0) * Math.Cos(rotateRads) -
                        transformation.WidthScale * avgX * Math.Cos(rotateRads) + 
                        avgX * Math.Cos(rotateRads) -
                        (centerOfRotation?.Y ?? 0) * Math.Sin(rotateRads) -
                        transformation.HeightScale * avgY * Math.Sin(rotateRads) + 
                        avgY * Math.Sin(rotateRads)),
                    (float) ((centerOfRotation?.Y ?? 0) -
                        (centerOfRotation?.Y ?? 0) * Math.Cos(rotateRads) -
                        transformation.HeightScale * avgY * Math.Cos(rotateRads) + 
                        avgY * Math.Cos(rotateRads) +
                        (centerOfRotation?.X ?? 0) * Math.Sin(rotateRads) +
                        transformation.WidthScale * avgX * Math.Sin(rotateRads) - 
                        avgX * Math.Sin(rotateRads)),
                    1
                }
            };

            var newTrapeze = new Trapeze(Points.Transform(transformMatrix));
            
            return newTrapeze;
        }

        public CanvasGeometry DrawShape(ICanvasResourceCreator canvasResourceCreator)
        {
            return CanvasGeometry.CreatePolygon(canvasResourceCreator, Points.Select(x => (Vector2)x).ToArray());
        }

        public ObservableVector2[] Points { get; }

        public void SetPoints(IEnumerable<ObservableVector2> points)
        {
            var pointsArray = points as ObservableVector2[] ?? points.ToArray();
            for (var i = 0; i < Points.Length; ++i)
            {
                Points[i].CopyPoint(pointsArray[i] ?? new ObservableVector2());
            }
        }
    }
}
