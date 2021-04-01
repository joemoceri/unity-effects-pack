namespace APPack.Effects
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class APGameObjectExtensions
    {
        public static IList<T> GetListOfComponents<T>(this GameObject obj, bool includeChildren) where T : Component
        {
            var result = new List<T>();
            var current = obj.GetComponent<T>();
            if (current != null)
                result.Add(current);

            if (includeChildren)
                result.AddRange(obj.GetComponentsInChildren<T>());

            return result;
        }
    }
}