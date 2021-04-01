using APPack.Effects;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class APDemo : MonoBehaviour
{
    public AnimationCurve EaseCurve;
    public AnimationCurve StraightCurve;
    public AnimationCurve SineCurve;
    public Dropdown EffectDropdown;
    public Dropdown CurveDropdown;
    public Dropdown WrapModeDropdown;
    public Slider LengthSlider;
    private APEffectBase[] Effects;
    private APEffectBase CurrentEffect;
    
    void Start ()
    {
        Effects = GetComponentsInChildren<APEffectBase>();
        EffectDropdown.onValueChanged.AddListener(EffectDropdownChanged);
        CurveDropdown.onValueChanged.AddListener(CurveDropdownChanged);
        WrapModeDropdown.onValueChanged.AddListener(WrapModeChanged);
        LengthSlider.onValueChanged.AddListener(LengthSliderChanged);
        
        SetCurrentEffect(APEffect.Move);
        
        EffectDropdownChanged(EffectDropdown.value);
	}

    private void EffectDropdownChanged(int option)
    {
        var effect = CurrentEffect.Effect();
        switch (option)
        {
            case 0: // move
                effect = APEffect.Move;
                break;
            case 1: // fade
                effect = APEffect.Fade;
                break;
            case 2: // rotate
                effect = APEffect.Rotate;
                break;
            case 3: // scale
                effect = APEffect.Scale;
                break;
            case 4: // squash and stretch
                effect = APEffect.SquashStretch;
                break;
            case 5: // color
                effect = APEffect.Color;
                break;
        }

        foreach(var e in Effects)
        {
            e.Stop();
        }

        SetCurrentEffect(effect);

        LengthSliderChanged(LengthSlider.value);
        WrapModeChanged(WrapModeDropdown.value);
        CurveDropdownChanged(CurveDropdown.value);

        switch (effect)
        {
            case APEffect.Move:
                CurveDropdown.value = 0;
                LengthSlider.value = 3f;
                break;
            case APEffect.SquashStretch:
                CurveDropdown.value = 2;
                LengthSlider.value = 3f;
                break;
            default:
                CurveDropdown.value = 0;
                LengthSlider.value = 2f;
                break;
        }

        var rend = GetComponent<Renderer>();
        var color = rend.sharedMaterial.color;
        rend.sharedMaterial.color = new Color(color.r, color.g, color.b, 1f);
        transform.position = new Vector3(-5f, 0f, 0f);

        CurrentEffect.ControlType = CurrentEffect.ControlType == APControlType.Nothing ? APControlType.Play : CurrentEffect.ControlType;

        CurrentEffect.Play();
    }

    private void SetCurrentEffect(APEffect effect)
    {
        CurrentEffect = Effects.Single(e => e.Effect() == effect);
    }

    private void LengthSliderChanged(float length)
    {
        CurrentEffect.Length = length;
    }

    private void WrapModeChanged(int option)
    {
        switch (option)
        {
            case 0: // ping pong
                CurrentEffect.WrapMode = APWrapMode.PingPong;
                break;
            case 1: // Loop
                CurrentEffect.WrapMode = APWrapMode.Loop;
                break;
        }
    }

    private void CurveDropdownChanged(int option)
    {
        switch (option)
        {
            case 0: // ease
                CurrentEffect.Curve = EaseCurve;
                break;
            case 1: // straight
                CurrentEffect.Curve = StraightCurve;
                break;
            case 2: // sine
                CurrentEffect.Curve = SineCurve;
                break;
        }
    }
}
