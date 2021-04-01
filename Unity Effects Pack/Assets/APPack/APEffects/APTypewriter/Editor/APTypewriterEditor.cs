namespace APPack.Effects
{
    using UnityEditor;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(APTypewriter))]
    public class APTypewriterEditor : APEffectBaseEditor
    {
        public override void OnInspectorGUI()
        {
            PreInspectorGUI();

            DisableAffectChildren = true;
            DisableContinuous = true;
            DisableCurve = true;

            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.LabelField("Typewriter Options", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("TypewriterType"), new GUIContent("Type"));

            if (serializedObject.FindProperty("TypewriterType").enumValueIndex == 0 /*HCUITypewriterType.Write*/)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("TextToWrite"), new GUIContent("Value to be written"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("From"), new GUIContent("From Which Direction?"));

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();

            PostInspectorGUI();
        }
    }
}