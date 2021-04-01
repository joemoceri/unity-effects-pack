namespace APPack.Effects
{
    using System;
    using UnityEngine;

    public static class APVector3Extensions
    {
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static Vector3 Round(this Vector3 vector, int places)
        {
            var x = Convert.ToSingle(Math.Round(vector.x, places));
            var y = Convert.ToSingle(Math.Round(vector.y, places));
            var z = Convert.ToSingle(Math.Round(vector.z, places));
            return new Vector3(x, y, z);
        }
    }
}
