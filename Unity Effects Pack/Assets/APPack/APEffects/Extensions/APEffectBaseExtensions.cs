namespace APPack.Effects
{
    using System.Collections.Generic;
    using System.Linq;

    public static class APEffectBaseExtensions
    {
        public static T GetEffect<T>(this IEnumerable<APEffectBase> effects, APEffect effect) where T : APEffectBase
        {
            return (T)effects.SingleOrDefault(e => e.Effect() == effect);
        }

        public static APEffectBase GetEffect(this IEnumerable<APEffectBase> effects, APEffect effect)
        {
            return effects.SingleOrDefault(e => e.Effect() == effect);
        }
    }
}