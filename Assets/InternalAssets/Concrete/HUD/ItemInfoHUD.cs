using DG.Tweening;
using SouthBasement.HUD.Base;
using SouthBasement.InventorySystem.ItemBase;
using TMPro;
using UnityEngine;

namespace SouthBasement
{
    public class ItemInfoHUD : MonoBehaviour, IWindow
    {
        [field: SerializeField] private Transform panel;
        [field: SerializeField] private TMP_Text text;

        private Vector2 _startPosition;
        
        private Tween _openTween;
        private Tween _closeTween;

        public bool CurrentlyOpened => panel.gameObject.activeSelf;
        public string currentItemID;

        public void SetItem(Item item)
        {
            text.text = item.GetStatsDescription();
            currentItemID = item.ItemID;
        }

        private void Awake()
        {
            _startPosition = panel.position;
            Close();
        }

        public void Open()
        { 
            if(_closeTween != null && _closeTween.active) 
                _closeTween.Kill();
            
            panel.gameObject.SetActive(true);
            _openTween = panel.DOMoveX(_startPosition.x, 0.2f);
        }

        public void Close()
        {
            _closeTween = panel.DOMoveX(_startPosition.x - 500f, 0.2f)
                .OnComplete(() => panel.gameObject.SetActive(false));
        }
        

        public void UpdateWindow() { }

        public void ResetItem(Item item)
        {
            if(item.ItemID == currentItemID)
                Close();
        }
    }
}
