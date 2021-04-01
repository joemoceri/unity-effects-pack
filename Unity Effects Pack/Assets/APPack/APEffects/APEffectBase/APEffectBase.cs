namespace APPack.Effects
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [Serializable]
    public abstract class APEffectBase : MonoBehaviour
    {
        public bool useThisObject = false;
        public bool randomStartDelay = false;
        public bool randomLength = false;

        public AnimationCurve originalCurve;

        [SerializeField]
        public List<APEventModel> Events = new List<APEventModel>();        
        public GameObject Target;
        public float OnStartDelay;
        public float Length;
        public AnimationCurve Curve = new AnimationCurve();
        public APWrapMode WrapMode = APWrapMode.Loop;
        public bool Continuous;
        public bool AffectChildren;
        public APControlType OnStartControlType;

        // These properties are the equivalent to read only for Unity
        public APControlType ControlType { get; set; } 
        public float NormalizedTimeValue { get; private set; }
        public bool Running { get; private set; }

        public abstract APEffect Effect();
        protected abstract void Activate();

        private void Awake()
        {
            OnAwake();
        }
        
        private void Start()
        {
            OnStart();
        }

        private void OnEnable()
        {
            OnStart();
        }

        protected virtual void OnAwake()
        {

        }
        
        protected virtual void OnStart()
        {
            if (useThisObject)
            {
                Target = gameObject;
            }

            if (randomStartDelay)
            {
                OnStartDelay = UnityEngine.Random.Range(0f, 1f);
            }

            if (randomLength)
            {
                Length = UnityEngine.Random.Range(1f, 2f);
            }

            ControlType = APControlType.Play; // Every effect starts out as if the intended effect is meant for play forward

            if (Curve.keys.Length != 0)
            {
                var startKeyTime = Curve.keys.First().time;
                var endKeyTime = Curve.keys.Last().time;
                if (startKeyTime != 0f || endKeyTime != 1f)
                    Curve = Curve.NormalizeCurve();
            }

            switch (OnStartControlType)
            {
                case APControlType.Play:
                    Play(OnStartDelay);
                    break;
                case APControlType.Reverse:
                    Reverse(OnStartDelay);
                    break;
            }
        }
        

        public void Play(float delay = 0f)
        {
            if (delay == 0f)
                DoAction(APControlType.Play);
            else
                StartCoroutine(DelayedTimer(DoAction, APControlType.Play, delay));
        }
        
        public void Reverse(float delay = 0f)
        {
            //if (new List<Type> { typeof(HCUIRepeat), typeof(HCUITypewriter), typeof(HCUIProgressBar) }.Any(e => e == GetType()))
            //    throw new NotSupportedException(string.Format("{0} does not support reverse capability!", GetType().FullName));

            if (delay == 0f)
                DoAction(APControlType.Reverse);
            else
                StartCoroutine(DelayedTimer(DoAction, APControlType.Reverse, delay));
        }

        private void DoAction(APControlType type)
        {
            if (Length == 0)
                throw new InvalidOperationException("Effect Length cannot be zero!");

            if (ControlType != type)
                ReverseControlType();

            Events.ForEach(e => e.Play = true);
            // stop any previous running coroutines
            Stop();
            Activate();
        }

        private IEnumerator DelayedTimer(Action<APControlType> action, APControlType type, float delay)
        {
            yield return new WaitForSeconds(delay);
            action(type);
        }

        protected IEnumerator EffectTimer(float length)
        {
            NormalizedTimeValue = 0f;
            Running = true;
            var curTime = 0f;
            while (NormalizedTimeValue < 1f || Continuous)
            {
                var endTime = Length;
                NormalizedTimeValue = Mathf.Clamp(curTime / endTime, 0f, 1f);
                curTime += Time.deltaTime;
                Change(NormalizedTimeValue);
                yield return null;

                if (Continuous && NormalizedTimeValue == 1f)
                {
                    Events.ForEach(e => e.Play = true);

                    if (WrapMode == APWrapMode.PingPong)
                        ReverseControlType();

                    curTime = 0f;
                    NormalizedTimeValue = 0f;
                    OnLoopCycleFinish();
                }
            }
            Running = false;
            Finish();
        }

        public void Stop()
        {
            StopAllCoroutines();
        }

        protected virtual void OnLoopCycleFinish() { }

        private void ReverseControlType()
        {
            ControlType = ControlType == APControlType.Reverse ? APControlType.Play : APControlType.Reverse;
            Curve = Curve.ReverseCurve();
        }

        private void Change(float time)
        {
            for (int i = 0; i < Events.Count; i++)
            {
                if (Events[i].ControlType == ControlType && Events[i].EventType == APEventType.OnChange)
                {
                    if (time >= Events[i].Percentage && Events[i].Play)
                    {
                        Events[i].Play = false;
                        Events[i].E.Invoke();
                    }
                }
            }
        }

        private void Finish()
        {
            for (int i = 0; i < Events.Count; i++)
            {
                if (Events[i].ControlType == ControlType && Events[i].EventType == APEventType.OnFinish)
                    Events[i].E.Invoke();
            }
        }
    }
}