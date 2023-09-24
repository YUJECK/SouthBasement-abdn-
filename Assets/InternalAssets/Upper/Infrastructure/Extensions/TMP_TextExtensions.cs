using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SouthBasement.Extensions
{
    public static class TMPTextExtensions 
    {
        public static TextTypingCoroutine TypeText(this TMP_Text text, string textToShow, float showRate, Action onTyped = null)
        {
            return new TextTypingCoroutine(text.StartCoroutine(TextShow(text, textToShow, showRate, onTyped)));
        }
        
        public static void StopTypingText(this TMP_Text text, TextTypingCoroutine coroutine)
        { 
            text.StopCoroutine(coroutine.TypingCoroutine);
        }
        
        private static IEnumerator TextShow(TMP_Text text, string textToShow, float showRate, Action onTyped)
        {
            text.text = "";
            
            foreach (var letter in textToShow)
            {
                text.text += letter;
                onTyped?.Invoke();
                
                yield return new WaitForSeconds(showRate);
            }
        }
    }
    
    public sealed class TextTypingCoroutine
    {
        public readonly Coroutine TypingCoroutine;

        public TextTypingCoroutine(Coroutine typingCoroutine)
        {
            TypingCoroutine = typingCoroutine;
        }
    }
}