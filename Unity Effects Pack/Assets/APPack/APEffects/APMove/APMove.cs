namespace APPack.Effects
{
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(Transform))]
    public class APMove : APEffectBase
    {
        private Vector3 OriginalStartPosition;
        private Vector3 OriginalEndPosition;
    
        public Vector3 EndPosition;
        public APMoveMotionType MotionType;
        public APMoveMovementType MovementType;
        public APMoveMovementDirection MovementDirection = APMoveMovementDirection.Positive;
        public float SineOscillations;
        public float SineAmplitude;
        public bool SineX, SineY, SineZ;
        public float CircularStartingAngle;
        public float CircularTurns;
        public float CircularRadius;
        public bool CircularX, CircularY, CircularZ;

        public override APEffect Effect()
        {
            return APEffect.Move;
        }

        private void Setup()
        {
            var anchoredPosition = Target.GetComponent<Transform>().localPosition;
            OriginalStartPosition = anchoredPosition;
            OriginalEndPosition = EndPosition;
            switch (MotionType)
            {
                case APMoveMotionType.Absolute:
                    break;
                case APMoveMotionType.Relative:
                    EndPosition = OriginalEndPosition + anchoredPosition;
                    break;
            }
        }

        protected override void Activate()
        {
            Setup();
            var rect = Target.GetComponent<Transform>();
            var startPosition = OriginalStartPosition;
        
            StartCoroutine(EffectTimer(Length));
            switch (MovementType)
            {
                case APMoveMovementType.Linear:
                case APMoveMovementType.Sine:
                    StartCoroutine(ApplyLinearMovement(rect, startPosition));
                    break;
                case APMoveMovementType.Circular:
                    StartCoroutine(ApplyCircularMovement(rect, Vector3.zero));
                    break;
            }
        }

        private IEnumerator ApplyLinearMovement(Transform target, Vector3 startPosition)
        {
            while (Running)
            {
                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                var result = Vector3.Lerp(startPosition, EndPosition, curveValue);
            
                if (MovementType == APMoveMovementType.Sine)
                {
                    var sineCalculation = SineAmplitude * ((Mathf.Sin(curveValue * (Mathf.PI * SineOscillations)) * (int)MovementDirection));
                    var x = SineX ? result.x + sineCalculation : result.x;
                    var y = SineY ? result.y + sineCalculation : result.y; 
                    var z = SineZ ? result.z + sineCalculation : result.z;
                    result = new Vector3(x, y, z);
                }

                target.localPosition = result;
                
                yield return null;
            }
        }

        private IEnumerator ApplyCircularMovement(Transform target, Vector3 center)
        {
            // There may be some accuracy issues here, might not be a problem
            var angle = CircularStartingAngle * Mathf.Deg2Rad;
            while (Running)
            {
                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                angle += curveValue * (((Mathf.PI * (2f * CircularTurns)) / Length) * Time.smoothDeltaTime * (int)MovementDirection);

                // End position is 'center'
                var zCalculation = CircularX ? Mathf.Sin(angle) : CircularY ? Mathf.Cos(angle) : 1f;
                var x = CircularX ? EndPosition.x + (Mathf.Cos(angle) * CircularRadius) : EndPosition.x;
                var y = CircularY ? EndPosition.y + (Mathf.Sin(angle) * CircularRadius) : EndPosition.y;
                var z = CircularZ ? EndPosition.z + (zCalculation * CircularRadius) : EndPosition.z;

                target.localPosition = new Vector3(x, y, z);

                yield return null;
            }
        }

        protected override void OnLoopCycleFinish()
        {
            if(WrapMode == APWrapMode.PingPong && MovementType == APMoveMovementType.Circular)
                MovementDirection = MovementDirection == APMoveMovementDirection.Negative ? APMoveMovementDirection.Positive : APMoveMovementDirection.Negative;
        }

    }
}