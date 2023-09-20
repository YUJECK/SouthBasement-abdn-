using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SouthBasement.UI
{
    public sealed class UIClickableObject : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnLeftClick; 
        public event Action OnMiddleClick; 
        public event Action OnRightClick; 

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
                OnLeftClick?.Invoke();
            else if(eventData.button == PointerEventData.InputButton.Middle)
                OnMiddleClick?.Invoke();
            else if(eventData.button == PointerEventData.InputButton.Right)
                OnRightClick?.Invoke();
        }
    }
}