using System.Collections;
using System.Collections.Generic;
using NTC.GlobalStateMachine;
using SouthBasement.Localization;
using TMPro;
using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class RandomText : StateMachineUser
    {
        [SerializeField] private List<LocalizedString> texts;
        private TMP_Text _text;

        protected override void OnAwake()
        {
            _text = GetComponent<TMP_Text>();
            _text.text = "";
        }

        protected override void OnDied()
        {
            StartCoroutine(PrintText(GetRandomText()));
        }

        private IEnumerator PrintText(string textToPrint)
        {
            _text.text = "";

            foreach (var letter in textToPrint)
            {
                _text.text += letter;

                yield return new WaitForSeconds(0.05f);
            }
        }

        private string GetRandomText()
            => texts[Random.Range(0, texts.Count)].GetLocalized();
    }
}
