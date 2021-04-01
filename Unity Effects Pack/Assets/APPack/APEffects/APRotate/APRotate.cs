namespace APPack.Effects
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Transform))]
    public class APRotate : APEffectBase
    {
        public bool RandomRotation;
        public bool RandomDirection;

        public bool RotateX;
        public bool RotateY;
        public bool RotateZ;

        public override APEffect Effect()
        {
            return APEffect.Rotate;
        }
    
        protected override void Activate()
        {
            var rects = Target.GetListOfComponents<Transform>(AffectChildren);
            StartCoroutine(EffectTimer(Length));
            StartCoroutine(ApplyEffect(rects));
        }

        private IEnumerator ApplyEffect(IList<Transform> rects)
        {
            int? selection = null;
            if (RandomRotation)
            {
                var selections = new[] { 0, 1, 2 };
                selection = selections[Random.Range(0, selections.Length)];
            }

            int direction = 1;

            if (RandomDirection)
            {
                var selections = new[] { -1, 1 };
                direction = selections[Random.Range(0, selections.Length)];
            }

            while (Running)
            {
                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                UpdateEffect(rects, selection, direction, curveValue);
                yield return null;
            }
        }

        protected void UpdateEffect(IList<Transform> rects, int? selection, int direction, float curveValue)
        {
            for (int i = 0; i < rects.Count; i++)
            {
                rects[i].localRotation = GetRotation(selection, direction, curveValue);
            }
        }

        private Quaternion GetRotation(int? selection, int direction, float curveValue)
        {
            var rotateValue = curveValue * 360f;
            if (selection != null)
            {
                var x = selection == 0 ? 1 * direction : 0;
                var y = selection == 1 ? 1 * direction : 0;
                var z = selection == 2 ? 1 * direction : 0;
                var axis = new Vector3(x, y, z);
                return Quaternion.AngleAxis(rotateValue, axis);
            }
            else
            {
                var x = RotateX ? 1 * direction : 0;
                var y = RotateY ? 1 * direction : 0;
                var z = RotateZ ? 1 * direction : 0;
                var axis = new Vector3(x, y, z);
                return Quaternion.AngleAxis(rotateValue, axis);
            }
        }
    }
}