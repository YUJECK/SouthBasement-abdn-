using System.Collections;
using DG.Tweening;
using NTC.GlobalStateMachine;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace SouthBasement.TraderItemDescriptionHUD
{
    public sealed class TraderHUD : StateMachineUser
    {
        [SerializeField] private Transform panel;
        [SerializeField] private TMP_Text traderName;
        [SerializeField] private TMP_Text text;
        
        [SerializeField] private Transform enablePosition;
        [SerializeField] private Transform disablePosition;

        private Coroutine _textPrintingCoroutine;
        
        private Tween _disableTween;
        private Tween _enableTween;

        private const string ItemsTable = "Items";
        private const float MoveDuration = 0.2f;

        private void Start() => Disable();

        public void ShowItemInfo(Item item, string traderName)
        {
            Enable();
            
            if(_textPrintingCoroutine != null) 
                StopCoroutine(_textPrintingCoroutine);
            
            this.traderName.text = traderName;
            _textPrintingCoroutine = StartCoroutine(PrintText(item.ItemDescription.GetLocalized()));
        }

        private void Enable()
        {
            if(_disableTween != null && _disableTween.active)
                _disableTween.Kill();
            
            panel.gameObject.SetActive(true);
            _enableTween = panel.DOMoveY(enablePosition.position.y, MoveDuration);
        }

        public void Disable()
        {
            if (_enableTween != null && _enableTween.active)
                return;
            
            _disableTween = panel.DOMoveY(disablePosition.position.y, MoveDuration).OnComplete(() => panel.gameObject.SetActive(false));
        }

        protected override void OnFight()
        {
            Disable();
        }

        protected override void OnIdle()
        {
            Disable();
        }

        private IEnumerator PrintText(string newText)
        {
            text.text = "";
            
            foreach (var letter in newText)
            {
                text.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}