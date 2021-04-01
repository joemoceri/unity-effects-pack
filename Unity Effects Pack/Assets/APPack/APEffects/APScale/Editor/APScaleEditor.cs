namespace APPack.Effects
{
    using UnityEditor;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(APScale))]
    public class HCUIScaleEditor : APEffectBaseEditor
    {
        public override void OnInspectorGUI()
        {
            PreInspectorGUI();

            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.LabelField("Scale Options", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("RandomScale"), new GUIContent("Pick Random Scale"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("ScaleX"), new GUIContent("X"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("ScaleY"), new GUIContent("Y"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("ScaleZ"), new GUIContent("Z"));

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();

            PostInspectorGUI();
        }
    }
}