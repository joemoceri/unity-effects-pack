namespace APPack.Effects
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Transform))]
    public class APScale : APEffectBase
    {
        public bool RandomScale;

        public bool ScaleX;
        public bool ScaleY;
        public bool ScaleZ;

        public override APEffect Effect()
        {
            return APEffect.Scale;
        }

        protected override void Activate()
        {
            var rects = Target.GetListOfComponents<Transform>(AffectChildren);
            StartCoroutine(EffectTimer(Length));
            StartCoroutine(ApplyEffect(rects));
        }

        protected IEnumerator ApplyEffect(IList<Transform> graphics)
        {
            int? selection = null;
            if (RandomScale)
            {
                var selections = new[] { 0, 1, 2 };
                selection = selections[Random.Range(0, selections.Length)];
            }

            while (Running)
            {
                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                for (int i = 0; i < graphics.Count; i++)
                {
                    graphics[i].localScale = GetScale(graphics[i], curveValue, selection);
                }

                yield return null;
            }
        }

        private Vector3 GetScale(Transform rect, float curveValue, int? selection)
        {
            var x = ScaleX || (selection != null && selection.Value >= 0) ? 1f + curveValue : rect.localScale.x;
            var y = ScaleY || (selection != null && selection.Value >= 1) ? 1f + curveValue : rect.localScale.y;
            var z = ScaleZ || (selection != null && selection.Value >= 2) ? 1f + curveValue : rect.localScale.z;
        
            return new Vector3(x, y, z);
        }
    }
}