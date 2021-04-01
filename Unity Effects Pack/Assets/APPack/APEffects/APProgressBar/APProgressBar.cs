namespace APPack.Effects
{
    using System;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(RectTransform))]
    public class APProgressBar : APEffectBase
    {
        private Vector2 OriginalSize;
        public float CurValue;

        public APProgressBarDirection Direction;
        public float MaxValue;

        protected override void OnAwake()
        {
            OriginalSize = Target.GetComponent<RectTransform>().sizeDelta;
            base.OnAwake();
        }
    
        public float CurrentProgress { get { return 1f - Mathf.Abs((CurValue / MaxValue)); } }

        public override APEffect Effect()
        {
            return APEffect.ProgressBar;
        }

        public void UpdateProgress(float value)
        {
            CurValue = Mathf.Clamp(CurValue + value, MaxValue * -1f, 0f); // Make sure it stays within the bounds
            Play();
        }
    
        protected override void Activate()
        {
            var rect = Target.GetComponent<RectTransform>();

            UpdatePivot(rect);
            var startPosition = rect.sizeDelta;
            StartCoroutine(EffectTimer(Length));
            StartCoroutine(ApplyEffect(rect, startPosition, GetEndPosition(rect)));
        }

        private void UpdatePivot(RectTransform rect)
        {
            float maxX = OriginalSize.x / 2f;
            float maxY = OriginalSize.y / 2f;

            float minX = maxX * -1f;
            float minY = maxY * -1f;

            switch (Direction)
            {
                case APProgressBarDirection.Left:
                    rect.pivot = new Vector2(1f, rect.pivot.y);
                    rect.anchoredPosition = new Vector2(maxX, rect.anchoredPosition.y);
                    break;
                case APProgressBarDirection.Right:
                    rect.pivot = new Vector2(0f, rect.pivot.y);
                    rect.anchoredPosition = new Vector2(minX, rect.anchoredPosition.y);
                    break;
                case APProgressBarDirection.Top:
                    rect.pivot = new Vector2(rect.pivot.x, 1f);
                    rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, maxY);
                    break;
                case APProgressBarDirection.Bottom:
                    rect.pivot = new Vector2(rect.pivot.x, 0f);
                    rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, minY);
                    break;
            }
        }

        private Vector3 GetEndPosition(RectTransform rect)
        {
            var trueValue = (CurValue / MaxValue); // Gets the correct value based on the container width
            switch (Direction)
            {
                case APProgressBarDirection.Left:
                case APProgressBarDirection.Right:
                    var x = Mathf.Clamp(OriginalSize.x + (trueValue * OriginalSize.x), 0f, OriginalSize.x);
                    return new Vector2(x, rect.sizeDelta.y);
                case APProgressBarDirection.Top:
                case APProgressBarDirection.Bottom:
                    var endY = Mathf.Clamp(OriginalSize.y + (trueValue * OriginalSize.y), 0f, OriginalSize.y);
                    return new Vector2(rect.sizeDelta.x, endY);
            }

            throw new InvalidOperationException(string.Format("Cannot find specified {0}.", typeof(APProgressBarDirection).Name));
        }

        private IEnumerator ApplyEffect(RectTransform rect, Vector3 startPosition, Vector3 endPosition)
        {
            while (Running)
            {
                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                rect.sizeDelta = Vector2.Lerp(startPosition, endPosition, curveValue);
                yield return null;
            }
        }
    }
}