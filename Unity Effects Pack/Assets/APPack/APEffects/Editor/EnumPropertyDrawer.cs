namespace APPack
{
    using System;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(EnumPropertyDrawerAttribute))]
    public class EnumPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var result = EditorGUI.EnumPopup(position, label, 
                    (Enum)Enum.ToObject((attribute as EnumPropertyDrawerAttribute).enumType, property.intValue)
                );

            property.intValue = Convert.ToInt32(result);
        }
    }
}