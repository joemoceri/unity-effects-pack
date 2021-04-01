namespace APPack.Effects
{
    using System;
    using System.Collections;
    using UnityEngine.UI;

    public class APTypewriter : APEffectBase
    {
        public string TextToWrite;
        public APTypewriterType TypewriterType;
        public APTypewriterDirection From;

        public override APEffect Effect()
        {
            return APEffect.Typewriter;
        }
    
        protected override void Activate()
        {
            var text = Target.GetComponent<Text>();
            Func<Text, IEnumerator> action = null;
            switch (TypewriterType)
            {
                case APTypewriterType.Write:
                    text.text = string.Empty;
                    action = From == APTypewriterDirection.Left ? new Func<Text, IEnumerator>(WriteFromLeft) : new Func<Text, IEnumerator>(WriteFromRight);
                    break;
                case APTypewriterType.Erase:
                    action = From == APTypewriterDirection.Left ? new Func<Text, IEnumerator>(EraseFromLeft) : new Func<Text, IEnumerator>(EraseFromRight);
                    break;
            }

            StartCoroutine(EffectTimer(Length));
            StartCoroutine(action(text));
        }

        private IEnumerator WriteFromLeft(Text text)
        {
            var curIndex = (int)(NormalizedTimeValue * TextToWrite.Length);
            while (Running)
            {
                if (curIndex != (int)(NormalizedTimeValue * TextToWrite.Length))
                {
                    curIndex = (int)(NormalizedTimeValue * TextToWrite.Length);
                    text.text = TextToWrite.Substring(0, curIndex);
                }

                yield return null;
            }
        }

        private IEnumerator WriteFromRight(Text text)
        {
            var curIndex = (int)((1f - NormalizedTimeValue) * TextToWrite.Length);
            while (Running)
            {
                if (curIndex != (int)((1f - NormalizedTimeValue) * TextToWrite.Length))
                {
                    curIndex = (int)((1f - NormalizedTimeValue) * TextToWrite.Length);
                    text.text = TextToWrite.Remove(0, curIndex);
                }

                yield return null;
            }
        }

        private IEnumerator EraseFromLeft(Text text)
        {
            var textToErase = text.text;
            var curIndex = (int)(NormalizedTimeValue * textToErase.Length);
            while (Running)
            {
                if (curIndex != (int)(NormalizedTimeValue * textToErase.Length))
                {
                    curIndex = (int)(NormalizedTimeValue * textToErase.Length);
                    text.text = textToErase.Remove(0, curIndex);
                }

                yield return null;
            }
        }

        private IEnumerator EraseFromRight(Text text)
        {
            var textToErase = text.text;
            var curIndex = (int)((1f - NormalizedTimeValue) * textToErase.Length);
            while (Running)
            {
                if (curIndex != (int)((1f - NormalizedTimeValue) * textToErase.Length))
                {
                    curIndex = (int)((1f - NormalizedTimeValue) * textToErase.Length);
                    text.text = textToErase.Substring(0, curIndex);
                }

                yield return null;
            }
        }
    }
}