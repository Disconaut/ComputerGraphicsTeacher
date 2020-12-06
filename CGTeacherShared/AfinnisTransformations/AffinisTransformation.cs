using System;
using System.Numerics;

namespace CGTeacherShared.AfinnisTransformations
{
    public static class AffinisTransformation
    {
      
        public static Vector2 Rotate(this Vector2 point, float engel)
        {
            point.X = point.X * (float) Math.Cos(engel) - point.Y * (float) Math.Sin(engel);
            point.Y = point.X * (float) Math.Sin(engel) + point.Y * (float) Math.Cos(engel);
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
            return point.Move(new Vector2(offsetX,offsetY));
        }

        public static Vector2 Zoom(this Vector2 point, float widthScale, float heightScale, float scaleCenterX, float scaleCenterY)
        {
            return point.Zoom(new Vector2(widthScale, heightScale), new Vector2( scaleCenterX,  scaleCenterY));

        }
        public static Vector2 Zoom(this Vector2 point, Vector2 scaleVector, Vector2 scaleCenter)
        {
            point.X += (point.X - scaleCenter.X) * scaleVector.X;
            point.Y += (point.Y - scaleCenter.Y) * scaleVector.Y;
            return point;
        }
    }
}