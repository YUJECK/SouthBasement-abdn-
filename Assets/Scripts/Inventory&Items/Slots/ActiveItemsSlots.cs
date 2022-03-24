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

    private InventoryManager inventoryManager;
    private Player playerController;

    //Использовние активки на зажатую клавишу
    [SerializeField] private float waitTime = 0f;
    [SerializeField] private float startTime;
    [SerializeField] private bool waitTimeSetted;
                    
    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        playerController = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(!isEmpty & isActiveSlot)
        {
            if(activeItem.waitTimeIfHave != 0 & !playerController.isSprinting)
            {
                if(!activeItem.isItemCharged)
                {
                    if(Input.GetMouseButton(1) & Time.time >= activeItem.GetNextTime())
                    {
                        SetNewWaitTime();
                        waitTime = Time.time - startTime;
                        inventoryManager.activeItemChargeSlider.value = waitTime;

                        if(activeItem.waitTimeIfHave <= waitTime) // Если активка зарядилась
                            activeItem.isItemCharged = true;
                    }
                    else //Сброс зарядки если кнопка была отпущена
                        ResetWaitTime();
                }

                //Использование активки после того как она зарядилась
                if(Input.GetMouseButtonUp(1) & activeItem.isItemCharged)
                    UseActveItem();
            }
            else if(playerController.isSprinting)
                ResetWaitTime();
                
            else // Если заряжать активку не надо
            {
                if(Input.GetMouseButtonDown(1) & Time.time >= activeItem.GetNextTime() & !playerController.isSprinting)
                    UseActveItem();
            }

            // Визуализация перезарядки активки
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
        ResetWaitTime();
        activeItem = null;
        isEmpty = true;
        slotIcon.sprite = FindObjectOfType<GameManager>().hollowSprite;
    }

    private void SetNewWaitTime() // 
    {
        if(!waitTimeSetted)
        {
            inventoryManager.activeItemChargeSlider.maxValue = activeItem.waitTimeIfHave;
            waitTime = 0f;
            startTime = Time.time;
            waitTimeSetted = true;
        }
    }
    private void ResetWaitTime() // Сброс всех показателей зарядки
    {
        waitTime = 0f;
        startTime = 0f;
        waitTimeSetted = false;
        activeItem.isItemCharged = false;
        inventoryManager.activeItemChargeSlider.value = 0f;
    }


    private void UseActveItem()
    {
        activeItem.ItemAction.Invoke();
        activeItem.SetNextTime();
        slider.maxValue = activeItem.GetNextTime() - Time.time;
        ResetWaitTime();    
    }
}
