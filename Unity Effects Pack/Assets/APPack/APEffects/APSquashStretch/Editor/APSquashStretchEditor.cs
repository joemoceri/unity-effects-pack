namespace APPack.Effects
{
    using UnityEditor;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(APSquashStretch))]
    public class APSquashStretchEditor : APEffectBaseEditor
    {
        protected override string CurveTooltip
        {
            get
            {
                return "Positive determines the y squash. Negative determines the x squash.";
            }
        }

        public override void OnInspectorGUI()
        {
            PreInspectorGUI();

            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.LabelField("Squash and Stretch Options", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Stretchiness"), new GUIContent("Stretchiness", "How much it gets pulled apart"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Denseness"), new GUIContent("Denseness", "How much remains after"));

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();

            PostInspectorGUI();
        }
    }
}