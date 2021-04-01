namespace APPack.Effects
{
    using System;
    using System.Collections;
    using UnityEngine;

    [Serializable]
    [RequireComponent(typeof(RectTransform))]
    public class APScroll : APEffectBase
    {
        public override APEffect Effect()
        {
            return APEffect.Scroll;
        }
    
        protected override void Activate()
        {
            StartCoroutine(EffectTimer(Length));
            StartCoroutine(ApplyEffect(Target.GetComponent<RectTransform>()));
        }

        private IEnumerator ApplyEffect(RectTransform rect)
        {
            var startPosition = rect.anchoredPosition;
            while (Running)
            {
                var parentWidth = transform.parent.GetComponent<RectTransform>().sizeDelta.x;
                var endPosition = new Vector2(rect.sizeDelta.x - parentWidth, rect.anchoredPosition.y);

                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                rect.anchoredPosition = Vector2.Lerp(startPosition, endPosition, curveValue);

                yield return null;
            }
        }
    }
}