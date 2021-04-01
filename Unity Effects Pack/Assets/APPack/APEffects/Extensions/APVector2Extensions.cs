namespace APPack.Effects
{
    using UnityEngine;

    public static class APVector2Extensions
    {
        public static Vector2 UpdateX(this Vector2 vector2, float x)
        {
            return new Vector2(vector2.x + x, vector2.y);
        }

        public static Vector2 UpdateY(this Vector2 vector2, float y)
        {
            return new Vector2(vector2.x, vector2.y + y);
        }

        public static Vector3 UpdateX(this Vector3 vector, float x)
        {
            return new Vector3(x, vector.y, vector.z);
        }

        public static Vector3 UpdateZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static Vector2 Update(this Vector2 vector2, float x, float y)
        {
            return new Vector2(vector2.x + x, vector2.y + y);
        }

        public static Vector2 ChangeX(this Vector2 vector2, float x)
        {
            return new Vector2(x, vector2.y);
        }

        public static Vector2 ChangeY(this Vector2 vector2, float y)
        {
            return new Vector2(vector2.x, y);
        }

        public static Vector2 Change(this Vector2 vector2, float x, float y)
        {
            return new Vector2(x, y);
        }
    }
}