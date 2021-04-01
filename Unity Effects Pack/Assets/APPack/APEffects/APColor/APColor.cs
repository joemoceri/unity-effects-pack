namespace APPack.Effects
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class APColor : APEffectBase
    {
        private Color OriginalColor;
        public Color[] Colors;
        public bool RandomizeOrder;
        private Gradient Gradient = new Gradient();

        public override APEffect Effect()
        {
            return APEffect.Color;
        }

        protected override void OnAwake()
        {
            var graphic = Target.GetComponent<Graphic>();
            var spriteRenderer = Target.GetComponent<SpriteRenderer>();
            var renderer = Target.GetComponent<Renderer>();

            if (graphic != null)
                OriginalColor = graphic.color;
            else if (spriteRenderer != null)
                OriginalColor = spriteRenderer.color;
            else if (renderer != null)
                OriginalColor = renderer.sharedMaterial.color;
            else
                throw new InvalidOperationException("Object is missing a component that has a color property");

            base.OnAwake();
        }

        private void Setup()
        {
            var graphic = Target.GetComponent<Graphic>();
            var spriteRenderer = Target.GetComponent<SpriteRenderer>();
            var renderer = Target.GetComponent<Renderer>();

            if (graphic != null)
                graphic.color = OriginalColor;
            else if (spriteRenderer != null)
                spriteRenderer.color = OriginalColor;
            else if (renderer != null)
                renderer.sharedMaterial.color = OriginalColor;
            else
                throw new InvalidOperationException("Object is missing a component that has a color property");

            if (Colors.Length < 2)
                throw new InvalidOperationException("You must have at least two colors.");

            var timeBetween = 1f / Colors.Length;

            var gck = new List<GradientColorKey>();
            var gak = new List<GradientAlphaKey>();
            var time = timeBetween / 2f;

            for (var i = 0; i < Colors.Length; i++)
            {
                gck.Add(new GradientColorKey(Colors[i], time));
                gak.Add(new GradientAlphaKey(Colors[i].a, time));
                if (i == Colors.Length - 1)
                    time -= timeBetween / 2f;
                time += timeBetween;
            }

            if (RandomizeOrder)
            {
                gck.Shuffle();
                gak.Shuffle();
            }

            Gradient.SetKeys(gck.ToArray(), gak.ToArray());
        }

        protected override void Activate()
        {
            Setup();
            var graphic = Target.GetComponent<Graphic>();
            var spriteRenderer = Target.GetComponent<SpriteRenderer>();
            var renderer = Target.GetComponent<Renderer>();

            if (graphic != null)
            {
                StartCoroutine(EffectTimer(Length));
                StartCoroutine(ApplyEffect(graphic));
            }
            else if (spriteRenderer != null)
            {
                StartCoroutine(EffectTimer(Length));
                StartCoroutine(ApplyEffect(spriteRenderer));
            }
            else if (renderer != null)
            {
                StartCoroutine(EffectTimer(Length));
                StartCoroutine(ApplyEffect(renderer.sharedMaterial));
            }

        }
    
        private IEnumerator ApplyEffect(SpriteRenderer spriteRenderer)
        {
            while (Running)
            {
                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                spriteRenderer.color = Gradient.Evaluate(curveValue);
                yield return null;
            }
        }

        private IEnumerator ApplyEffect(Graphic graphic)
        {

            while (Running)
            {
                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                graphic.color = Gradient.Evaluate(curveValue);
                yield return null;
            }
        }

        private IEnumerator ApplyEffect(Material mat)
        {
            while (Running)
            {
                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                mat.color = Gradient.Evaluate(curveValue);
                yield return null;
            }
        }
    }
}