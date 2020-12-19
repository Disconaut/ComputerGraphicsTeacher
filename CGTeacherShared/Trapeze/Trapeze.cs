using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CGTeacherShared.Trapeze
{
    public static class Trapeze
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
            point.X += (point.X - scaleCenter.X) * scaleVector.X;
            point.Y += (point.Y - scaleCenter.Y) * scaleVector.Y;
            return point;
        }
    }
}
