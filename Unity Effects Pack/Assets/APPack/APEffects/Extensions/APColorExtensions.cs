namespace APPack.Effects
{
    using UnityEngine;

    public static class APColorExtensions
    {
        public static Color ChangeAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, Mathf.Clamp(alpha, 0f, 1f));
        }

        public static Color UpdateAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, Mathf.Clamp(color.a + alpha, 0f, 1f));
        }
    }
}