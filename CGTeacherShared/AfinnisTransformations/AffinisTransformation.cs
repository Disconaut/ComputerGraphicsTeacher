using System;
using System.Linq;
using System.Numerics;
using CGTeacherShared.Shared.Vector;

namespace CGTeacherShared.AfinnisTransformations
{
    public static class AffinisTransformation
    {
        public static Vector2 Rotate(this Vector2 point, float angle, float centerX, float centerY)
        {
            return point.Rotate(angle, new Vector2(centerX, centerY));
        }

        public static Vector2 Rotate(this Vector2 point, float angle, Vector2 center)
        {
            var rads = Math.PI * angle / 180;
            var vector = point - center;
            point.X = vector.X * (float)Math.Cos(rads) - vector.Y * (float)Math.Sin(rads) + center.X;
            point.Y = vector.X * (float)Math.Sin(rads) + vector.Y * (float)Math.Cos(rads) + center.Y;
            return point;
        }

        public static Vector2 Move(this Vector2 point, Vector2 offsetVector)
        {
            point.X += offsetVector.X;
            point.Y += offsetVector.Y;
            return point;
        }

        public static Vector2 Move(this Vector2 point, float offsetX, float offsetY)
        {
            return point.Move(new Vector2(offsetX, offsetY));
        }

        public static Vector2 Zoom(this Vector2 point, float widthScale, float heightScale, float scaleCenterX, float scaleCenterY)
        {
            return point.Zoom(new Vector2(widthScale, heightScale), new Vector2(scaleCenterX, scaleCenterY));

        }
        public static Vector2 Zoom(this Vector2 point, Vector2 scaleVector, Vector2 scaleCenter)
        {
            point.X = (point.X - scaleCenter.X) * scaleVector.X + scaleCenter.X;
            point.Y = (point.Y - scaleCenter.Y) * scaleVector.Y + scaleCenter.Y;
            return point;
        }

        public static Vector2[] Transform(this Vector2[] points, float[][] transformMatrix)
        {
            var pointsMatrix = points.Select(x => new[] { x.X, x.Y, 1 }).ToArray();
            var pointsMatrixRows = pointsMatrix.Length;
            var pointsMatrixCols = pointsMatrix.FirstOrDefault()?.Length ?? 0;
            var transformMatrixRows = transformMatrix.Length;
            var transformMatrixCols = transformMatrix.FirstOrDefault()?.Length ?? 0;

            if (pointsMatrixCols != transformMatrixRows)
                throw new InvalidOperationException
                    ("Product is undefined. n columns of first matrix must equal to n rows of second matrix");

            var product = Enumerable
                .Range(0, pointsMatrixRows)
                .Select(x => new float[transformMatrixCols])
                .ToArray();

            for (var pRow = 0; pRow < pointsMatrixRows; pRow++)
            {
                for (var tCol = 0; tCol < transformMatrixCols; tCol++)
                {
                    for (var pCol = 0; pCol < pointsMatrixCols; pCol++)
                    {
                        product[pRow][tCol] +=
                            pointsMatrix[pRow][pCol] *
                            transformMatrix[pCol][tCol];
                    }
                }
            }

            return product.Select(x => new Vector2(x[0], x[1])).ToArray();
        }

        public static ObservableVector2[] Transform(this ObservableVector2[] points, float[][] transformMatrix)
        {
            var pointsMatrix = points.Select(x => new[] { (float)(x.X ?? 0), (float)(x.Y?? 0), 1 }).ToArray();
            var pointsMatrixRows = pointsMatrix.Length;
            var pointsMatrixCols = pointsMatrix.FirstOrDefault()?.Length ?? 0;
            var transformMatrixRows = transformMatrix.Length;
            var transformMatrixCols = transformMatrix.FirstOrDefault()?.Length ?? 0;

            if (pointsMatrixCols != transformMatrixRows)
                throw new InvalidOperationException
                    ("Product is undefined. n columns of first matrix must equal to n rows of second matrix");

            var product = Enumerable
                .Range(0, pointsMatrixRows)
                .Select(x => new float[transformMatrixCols])
                .ToArray();

            for (var pRow = 0; pRow < pointsMatrixRows; pRow++)
            {
                for (var tCol = 0; tCol < transformMatrixCols; tCol++)
                {
                    for (var pCol = 0; pCol < pointsMatrixCols; pCol++)
                    {
                        product[pRow][tCol] +=
                            pointsMatrix[pRow][pCol] *
                            transformMatrix[pCol][tCol];
                    }
                }
            }

            return product.Select(x => new ObservableVector2(x[0], x[1])).ToArray();
        }
    }
}