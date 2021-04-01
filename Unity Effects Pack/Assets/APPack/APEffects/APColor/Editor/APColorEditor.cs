namespace APPack.Effects
{
    using UnityEditor;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(APColor))]
    public class HCUIColorEditor : APEffectBaseEditor
    {
        public override void OnInspectorGUI()
        {
            PreInspectorGUI();

            DisableAffectChildren = true;

            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.LabelField("Color Options", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Colors"), true);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("RandomizeOrder"));

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();

            PostInspectorGUI();
        }
    }
}