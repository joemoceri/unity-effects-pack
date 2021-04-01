namespace APPack.Effects
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class APFade : APEffectBase
    {
        public override APEffect Effect()
        {
            return APEffect.Fade;
        }
    
        protected override void Activate()
        {
            var graphics = Target.GetListOfComponents<Graphic>(AffectChildren);
            var spriteRenderers = Target.GetListOfComponents<SpriteRenderer>(AffectChildren);
            var renderers = Target.GetListOfComponents<Renderer>(AffectChildren).ToList();

            if (graphics.Count > 0)
            {
                StartCoroutine(EffectTimer(Length));
                StartCoroutine(ApplyEffect(graphics));
            }
            else if (spriteRenderers.Count > 0)
            {
                StartCoroutine(EffectTimer(Length));
                StartCoroutine(ApplyEffect(spriteRenderers));
            }
            else if (renderers.Count > 0)
            {
                StartCoroutine(EffectTimer(Length));
                StartCoroutine(ApplyEffect(renderers));
            }
        }

        private IEnumerator ApplyEffect(IList<Renderer> renderers)
        {
            while (Running)
            {
                var curveValue = Curve.Evaluate(Mathf.Clamp(NormalizedTimeValue, 0f, 1f));

                for (int i = 0; i < renderers.Count; i++)
                    renderers[i].sharedMaterial.color = renderers[i].sharedMaterial.color.ChangeAlpha(curveValue);

                yield return null;
            }
        }

        private IEnumerator ApplyEffect(IList<SpriteRenderer> spriteRenderers)
        {
            while (Running)
            {
                var curveValue = Curve.Evaluate(Mathf.Clamp(NormalizedTimeValue, 0f, 1f));

                for (int i = 0; i < spriteRenderers.Count; i++)
                    spriteRenderers[i].color = spriteRenderers[i].color.ChangeAlpha(curveValue);

                yield return null;
            }
        }
    
        private IEnumerator ApplyEffect(IList<Graphic> graphics)
        {
            while (Running)
            {
                var curveValue = Curve.Evaluate(Mathf.Clamp(NormalizedTimeValue, 0f, 1f));

                for (int i = 0; i < graphics.Count; i++)
                    graphics[i].color = graphics[i].color.ChangeAlpha(curveValue);

                yield return null;
            }
        }

    }
}