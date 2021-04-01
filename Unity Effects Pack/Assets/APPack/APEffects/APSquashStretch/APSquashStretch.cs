namespace APPack.Effects
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Transform))]
    public class APSquashStretch : APEffectBase
    {
        [Range(0.1f, 3f)]
        public float Stretchiness;
        [Range(1f, 15f)]
        public float Denseness;

        public override APEffect Effect()
        {
            return APEffect.SquashStretch;
        }
    
        protected override void Activate()
        {
            var rects = Target.GetListOfComponents<Transform>(AffectChildren);
            StartCoroutine(EffectTimer(Length));
            StartCoroutine(ApplyEffect(rects));
        }

        private IEnumerator ApplyEffect(IList<Transform> graphics)
        {
            var originalGraphicsScales = new List<Vector3>();
            for(var i = 0; i < graphics.Count; i++)
            {
                var scale = graphics[i].localScale;
                scale = new Vector3(scale.x, scale.y, scale.z);
                originalGraphicsScales.Add(scale);
            }

            while (Running)
            {
                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                for (int i = 0; i < graphics.Count; i++)
                {
                    var positive = curveValue > 0;

                    var x = 1f + (positive ? -1f * curveValue / Denseness : Mathf.Abs(curveValue) * Stretchiness);
                    var y = 1f + (positive ? curveValue * Stretchiness : -1f * Mathf.Abs(curveValue) / Denseness);
                    var z = originalGraphicsScales[i].z;
                    
                    var signX = Mathf.Sign(originalGraphicsScales[i].x);
                    var signY = Mathf.Sign(originalGraphicsScales[i].y);

                    x = x * signX;
                    y = y * signY;

                    graphics[i].localScale = new Vector3(x, y, z);
                }

                yield return null;
            }
        }
    }
}