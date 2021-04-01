namespace APPack.Effects
{
    using UnityEditor;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(APRotate))]
    public class HCUIRotateEditor : APEffectBaseEditor
    {
        protected override string CurveTooltip
        {
            get
            {
                return "1 is a 360 positive rotation. -1 is a 360 negative rotation.";
            }
        }

        public override void OnInspectorGUI()
        {
            PreInspectorGUI();

            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.LabelField("Rotate Options", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("RandomRotation"), new GUIContent("Pick Random Rotation"));
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("RandomDirection"), new GUIContent("Pick Random Direction"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("RotateX"), new GUIContent("X"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("RotateY"), new GUIContent("Y"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("RotateZ"), new GUIContent("Z"));

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        
            PostInspectorGUI();
        }
    }
}