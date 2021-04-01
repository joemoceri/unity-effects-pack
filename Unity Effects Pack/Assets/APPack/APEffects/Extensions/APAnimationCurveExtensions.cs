namespace APPack.Effects
{
    using System.Linq;
    using UnityEngine;

    public static class APAnimationCurveExtensions
    {
        public static Keyframe[] ShiftKeyframesFromNegativeToZero(this AnimationCurve curve)
        {
            var shiftAmount = Mathf.Abs(curve.keys.Max(f => Mathf.Abs(f.time)));
            var result = curve.keys;

            for (int i = 0; i < curve.keys.Length; i++)
                result[i].time += shiftAmount;

            return result;
        }

        public static Keyframe[] ShiftKeyframesFromPositiveToZero(this AnimationCurve curve)
        {
            var shiftAmount = Mathf.Abs(curve.keys.Min(f => Mathf.Abs(f.time)));
            var result = curve.keys;

            for (int i = 0; i < curve.keys.Length; i++)
                result[i].time -= shiftAmount;

            return result;
        }

        public static AnimationCurve ReverseCurve(this AnimationCurve curve)
        {
            var keys = curve.keys.AsEnumerable().Reverse().ToList();
            var newKeys = new Keyframe[curve.keys.Length];
            var maxTangent = 0f;
            for (int i = 0; i < curve.keys.Length; i++)
            {
                newKeys[i].time = keys[i].time;
                newKeys[i].value = curve.keys[i].value;

                newKeys[i].inTangent = keys[i].inTangent;
                newKeys[i].outTangent = keys[i].outTangent;
                maxTangent = Mathf.Max(keys[i].inTangent, keys[i].outTangent);
            }

            curve.keys = newKeys;

            if (maxTangent != 0f)
            {
                for (int i = 0; i < curve.keys.Length; i++)
                    curve.SmoothTangents(i, 0);
            }

            return curve;
        }

        public static AnimationCurve CreateNewCurve(this AnimationCurve values)
        {
            var result = new AnimationCurve();
            var newKeys = new Keyframe[values.keys.Length];
            var maxTangent = 0f;
            for (int i = 0; i < values.keys.Length; i++)
            {
                newKeys[i].time = values[i].time;
                newKeys[i].value = values.keys[i].value;

                newKeys[i].inTangent = values[i].inTangent;
                newKeys[i].outTangent = values[i].outTangent;
                maxTangent = Mathf.Max(values[i].inTangent, values[i].outTangent);
            }

            result.keys = newKeys;

            if (maxTangent != 0f)
            {
                for (int i = 0; i < result.keys.Length; i++)
                    result.SmoothTangents(i, 0);
            }

            return result;
        }

        public static AnimationCurve NormalizeCurve(this AnimationCurve curve)
        {
            var firstKeyTime = curve.keys.First().time;
            if (firstKeyTime < 0f)
                curve.keys = ShiftKeyframesFromNegativeToZero(curve);
            else if (firstKeyTime > 0f)
                curve.keys = ShiftKeyframesFromPositiveToZero(curve);

            var endTime = curve.keys.Last().time;
            var maxTangent = 0f;
            var keys = new Keyframe[curve.keys.Length];
            for (int i = 0; i < curve.keys.Length; i++)
            {
                keys[i].time = curve.keys[i].time == endTime ? 1f : curve.keys[i].time / endTime;
                keys[i].value = curve.keys[i].value;
                keys[i].inTangent = curve.keys[i].inTangent;
                keys[i].outTangent = curve.keys[i].outTangent;
                maxTangent = Mathf.Max(curve.keys[i].inTangent, curve.keys[i].outTangent);
            }

            curve.keys = keys;

            if (maxTangent != 0f)
            {
                for (int i = 0; i < curve.keys.Length; i++)
                    curve.SmoothTangents(i, 0);
            }

            return curve;
        }
    }
}
