namespace APPack.Effects
{
    using UnityEditor;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(APProgressBar))]
    public class APProgressBarEditor : APEffectBaseEditor
    {
        public override void OnInspectorGUI()
        {
            PreInspectorGUI();

            DisableAffectChildren = true;
            DisableContinuous = true;

            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.LabelField("Progress Bar Options", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Direction"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MaxValue"));

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();

            PostInspectorGUI();
        }
    }
}