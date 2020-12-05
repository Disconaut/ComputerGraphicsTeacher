using System.Numerics;

namespace CGTeacherShared.AfinnisTransformations
{
    public static class AffinisTransformation
    {
        public static Vector2 Rotate(this Vector2 v)
        {
            return Vector2.One;
        }
        public static Vector2 Move( this Vector2 point, Vector2 offsetVector )
        {
            point.X += offsetVector.X;
            point.Y += offsetVector.Y;
            return point;
        }
        public static Vector2 Move(this Vector2 point, float offsetX, float offsetY)
        {
            return point.Move(new Vector2(offsetX,offsetY));
        }

        public static Vector2 Zoom(this Vector2 point)
        {

        }
    }
}