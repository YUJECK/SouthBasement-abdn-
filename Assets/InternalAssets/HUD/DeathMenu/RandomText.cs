using System.Collections;
using System.Collections.Generic;
using NTC.GlobalStateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace SouthBasement
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class RandomText : StateMachineUser
    {
        [SerializeField] private List<string> entryReferences;

        private const string TableName = "DeathPanelRandomText";
        private TMP_Text _text;

        protected override void OnAwake()
            => _text = GetComponent<TMP_Text>();

        protected override void OnDied()
        {
            var textToPrint 
                = new LocalizedString(TableName, GetEntryReference()).GetLocalizedString();

            StartCoroutine(PrintText(textToPrint));
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

        private string GetEntryReference()
            => entryReferences[Random.Range(0, entryReferences.Count)];
    }
}
