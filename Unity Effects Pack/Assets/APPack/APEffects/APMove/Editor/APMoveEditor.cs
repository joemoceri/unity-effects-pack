namespace APPack.Effects
{
    using UnityEditor;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(APMove))]
    public class APMoveEditor : APEffectBaseEditor
    {
        protected override string CurveTooltip
        {
            get
            {
                return "Determines the strength as a percentage of the movement from 0 to 1.";
            }
        }

        public override void OnInspectorGUI()
        {
            PreInspectorGUI();

            DisableAffectChildren = true;

            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.LabelField("Move Options", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("MotionType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("EndPosition"), new GUIContent("End Position"));
            var prop = serializedObject.FindProperty("MovementType");
            EditorGUILayout.PropertyField(prop);

            if (prop.intValue == 1) // Circular
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CircularStartingAngle"), new GUIContent("Starting Angle"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CircularRadius"), new GUIContent("Radius"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CircularTurns"), new GUIContent("Turns"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("MovementDirection"));
                EditorGUILayout.LabelField("Apply Circular Movement to each of the following");
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CircularX"), new GUIContent("X"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CircularY"), new GUIContent("Y"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CircularZ"), new GUIContent("Z"));
            }

            if (prop.intValue == 2) // Sine
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("SineOscillations"), new GUIContent("Oscillations"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("SineAmplitude"), new GUIContent("Amplitude"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("MovementDirection"));
                EditorGUILayout.LabelField("Apply Sine Movement to each of the following");
                EditorGUILayout.PropertyField(serializedObject.FindProperty("SineX"), new GUIContent("X"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("SineY"), new GUIContent("Y"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("SineZ"), new GUIContent("Z"));
            }
        
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();

            PostInspectorGUI();
        }
    }
}