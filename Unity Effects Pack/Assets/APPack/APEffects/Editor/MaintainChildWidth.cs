namespace APPack.Effects
{
    using UnityEditor;
    using UnityEngine;

    [ExecuteInEditMode]
    public class MaintainChildWidth : MonoBehaviour
    {
        void Update()
        {
            if (!EditorApplication.isPlaying)
            {
                var rectTransform = GetComponent<RectTransform>();
                foreach (RectTransform rect in transform)
                {
                    rect.sizeDelta = rectTransform.sizeDelta;
                }
            }
        }
    }
}