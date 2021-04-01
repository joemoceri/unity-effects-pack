namespace APPack.Effects
{
    using System;
    using UnityEditor;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(APEffectBase))]
    public class APEffectBaseEditor : Editor
    {
        protected bool DisableContinuous;
        protected bool DisableCurve;
        protected bool DisableAffectChildren;
        protected virtual string CurveTooltip { get { return string.Empty; } }
        private string BaseCurveTooltip = "Curve will be normalized if it isn't between 0 and 1 inclusive.";

        public void PreInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Target"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("useThisObject"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("randomStartDelay"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("randomLength"));

            EditorGUILayout.Space();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Each effects settings on game start will be used as the 'Play' settings.", EditorStyles.wordWrappedLabel);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("On Start", EditorStyles.boldLabel);
            var prop = serializedObject.FindProperty("OnStartControlType");
            EditorGUILayout.PropertyField(prop, new GUIContent("Action"));

            if (prop.intValue != 0)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("OnStartDelay"), new GUIContent("Delay"));

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(CurveTooltip + Environment.NewLine + BaseCurveTooltip, MessageType.Info);
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(DisableCurve);        
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Curve"));
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Length"), new GUIContent("Length"));

            EditorGUI.BeginDisabledGroup(DisableAffectChildren);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("AffectChildren"), new GUIContent("Affect Children"));
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(DisableContinuous);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Continuous"), new GUIContent("Continuous"));
            if (serializedObject.FindProperty("Continuous").boolValue)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("WrapMode"), new GUIContent("Wrap Mode"));

            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
        }

        public void PostInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);

            var events = serializedObject.FindProperty("Events");
            for (int i = 0; i < events.arraySize; i++)
            {
                if (GUILayout.Button(new GUIContent("X"), GUILayout.Width(25f)))
                {
                    events.DeleteArrayElementAtIndex(i);
                    continue;
                }

                if (events.arraySize > 0)
                {
                    EditorGUILayout.PropertyField(events.GetArrayElementAtIndex(i).FindPropertyRelative("ControlType"), new GUIContent("Action"));
                    EditorGUILayout.PropertyField(events.GetArrayElementAtIndex(i).FindPropertyRelative("EventType"), new GUIContent("Event Type"));
                    if (events.GetArrayElementAtIndex(i).FindPropertyRelative("EventType").enumValueIndex == 0 /*HCUIEventType.OnChanged*/)
                    {
                        EditorGUILayout.PropertyField(events.GetArrayElementAtIndex(i).FindPropertyRelative("Percentage"), new GUIContent("Percentage"));
                    }
                }

                if (i <= events.arraySize - 1)
                {
                    var p = events.GetArrayElementAtIndex(i);
                    if (p != null)
                    {
                        var propEvent = p.FindPropertyRelative("E");
                        if (propEvent != null)
                            EditorGUILayout.PropertyField(propEvent);
                    }
                }
            }
            if (GUILayout.Button(new GUIContent("Add Event")))
                events.InsertArrayElementAtIndex(events.arraySize);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
