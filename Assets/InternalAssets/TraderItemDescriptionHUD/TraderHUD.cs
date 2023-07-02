using System.Collections;
using DG.Tweening;
using SouthBasement.InventorySystem;
using TMPro;
using UnityEngine;

namespace SouthBasement.TraderItemDescriptionHUD
{
    public sealed class TraderHUD : MonoBehaviour
    {
        [SerializeField] private Transform panel;
        [SerializeField] private TMP_Text traderName;
        [SerializeField] private TMP_Text text;
        
        [SerializeField] private Transform enablePosition;
        [SerializeField] private Transform disablePosition;

        private Coroutine _textPrintingCoroutine;
        
        private Tween _disableTween;
        private Tween _enableTween;

        private void Start() => Disable();

        public void ShowItemInfo(Item item, string traderName)
        {
            Enable();
            
            if(_textPrintingCoroutine != null) StopCoroutine(_textPrintingCoroutine);
            
            this.traderName.text = traderName;
            _textPrintingCoroutine = StartCoroutine(PrintText(item.ItemDescription));
        }

        private void Enable()
        {
            if(_disableTween != null && _disableTween.active)
                _disableTween.Kill();
            
            _enableTween = panel.DOMoveY(enablePosition.position.y, 0.15f);
        }

        public void Disable()
        {
            if(_enableTween != null && _enableTween.active)
                _enableTween.Kill();
            
            _disableTween = panel.DOMoveY(disablePosition.position.y, 0.15f);
        }

        private IEnumerator PrintText(string newText)
        {
            text.text = "";
            
            foreach (var letter in newText)
            {
                text.text += letter;
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}