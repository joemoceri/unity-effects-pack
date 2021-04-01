namespace APPack.Effects
{
    using UnityEditor;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(APScroll))]
    public class APScrollEditor : APEffectBaseEditor
    {
        public override void OnInspectorGUI()
        {
            PreInspectorGUI();

            DisableAffectChildren = true;

            base.OnInspectorGUI();

            PostInspectorGUI();
        }
    }
}