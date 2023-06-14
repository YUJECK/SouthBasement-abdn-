using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace TheRat.InventorySystem
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("HUD/Inventory/ActiveItemSlot")]
    public sealed class ActiveItemSlot : InventorySlot<ActiveItem>
    {
        private ActiveItemUsage _activeItemUsage;
        [SerializeField] private GameObject isCurrent;

        [Inject]
        private void Construct(ActiveItemUsage activeItemUsage)
        {
            _activeItemUsage = activeItemUsage;
            _activeItemUsage.OnSelected += OnSelected;
        }

        private void OnSelected(ActiveItem item)
        {
            if(CurrentItem == null)
                return;

            if(item.ItemID == CurrentItem.ItemID)
                isCurrent.SetActive(true);
            else
                isCurrent.SetActive(false);
        }

        private void Awake()
        {
            ItemImage = GetComponent<Image>();
            GetComponentInParent<Button>().onClick.AddListener(SetCurrent);
            
            SetItem(null);
        }

        private void SetCurrent()
        {
            if(CurrentItem == null)
                return;
            
            _activeItemUsage.SetCurrent(CurrentItem);
        }
    }
}