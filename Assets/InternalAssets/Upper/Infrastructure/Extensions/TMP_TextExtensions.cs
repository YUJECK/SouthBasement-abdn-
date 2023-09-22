using System.Collections;
using TMPro;
using UnityEngine;

namespace SouthBasement.InternalAssets.Upper.Infrastructure.Extensions
{
    public static class TMP_TextExtensions 
    {
        public static void Show(this TMP_Text text, string textToShow, float showRate)
        {
            text.StartCoroutine(TextShow(text, textToShow, showRate));
        }

        private static IEnumerator TextShow(TMP_Text text, string textToShow, float showRate)
        {
            text.text = "";
            
            foreach (var letter in textToShow)
            {
                text.text += letter;
                yield return new WaitForSeconds(showRate);
            }
        }
    }
}