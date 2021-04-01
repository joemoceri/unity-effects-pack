namespace APPack.Effects
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    [Serializable]
    public class APEventModel
    {
        public APControlType ControlType;
        public APEventType EventType;
        [Range(0f, 1f)]
        public float Percentage;
        [SerializeField]
        public UnityEvent E;
        public bool Play;
    }
}