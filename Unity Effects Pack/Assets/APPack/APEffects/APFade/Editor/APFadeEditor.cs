namespace APPack.Effects
{
    using UnityEditor;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(APFade))]
    public class HCUIFadeEditor : APEffectBaseEditor
    {
        protected override string CurveTooltip
        {
            get
            {
                return "0 is 0% alpha. 1 is 100% alpha.";
            }
        }

        public override void OnInspectorGUI()
        {
            PreInspectorGUI();

            base.OnInspectorGUI();

            PostInspectorGUI();
        }
    }
}
