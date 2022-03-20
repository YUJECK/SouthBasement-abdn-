using UnityEngine;
using UnityEngine.UI;

public class ActiveItemsSlots : MonoBehaviour
{
    public ActiveItem activeItem;   // Предмет который лежит в этом слоте
    [SerializeField] private Image slotIcon;   // Иконка предмета который лежит в этом слоте
    [SerializeField] private Slider slider;   // Иконка предмета который лежит в этом слоте
    public GameObject objectOfItem;   // гейм обжект этого предмета

    public bool isEmpty; // Пустой ли этот слот
    public bool isActiveSlot; // Используется ли этот слот сейчас

    private void Update()
    {
        if(!isEmpty & isActiveSlot)
        {
            if(Input.GetMouseButtonDown(1) & Time.time >= activeItem.GetNextTime())
            {
                activeItem.ItemAction.Invoke();
                activeItem.SetNextTime();
                slider.maxValue = activeItem.GetNextTime() - Time.time;
            }
            slider.value = activeItem.GetNextTime() - Time.time;
        }
        
    }

    public void Add(ActiveItem newActiveItem, GameObject _objectOfItem) // Добавеление предмета
    {
        if(activeItem != null)
            Drop();

        activeItem = newActiveItem;
        objectOfItem = _objectOfItem;
        isEmpty = false;
        slotIcon.sprite = newActiveItem.sprite;
    }

    public void Drop() // Выброс предмета в игре
    {
        objectOfItem.SetActive(true);
        objectOfItem.transform.position = FindObjectOfType<Player>().GetComponent<Transform>().position;
        Remove();
    }
    
    public void Remove() // Удаление предмета из слота
    {
        activeItem = null;
        isEmpty = true;
        slotIcon.sprite = FindObjectOfType<GameManager>().hollowSprite;
    }
}
