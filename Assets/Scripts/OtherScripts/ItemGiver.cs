using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemGiver : MonoBehaviour
{
    [Header("Настройки выдачи предмета")]
    [SerializeField] private GameObject item; //Сам предмет
    [SerializeField] private int giveCount = 1; //Сам предмет
    [SerializeField] ItemClass itemClass = ItemClass.Food; //Класс предмета
    [SerializeField] public bool giveRightAway = false; //Дать сразу в интентарь/Заспавнить в точке
    [SerializeField] private Transform itemPos; //Точка спавна предмета(giveRightAway == true)
    [Header("Настройки триггера")]
    [SerializeField] private string triggerTag = "Player"; //Тег на который будет триггириться 
    [SerializeField] public bool changeSpriteOnTrigger = true; //Будет ли меняться спрайт
    [SerializeField] private Sprite defaultSprite; //Обычный спрайт
    [SerializeField] private Sprite triggerSprite; //Спрайт при триггере
    [SerializeField] public bool checkFromTriggerChecker = false; //Проверять ли триггер через TriggerChecker
    [SerializeField] private TriggerChecker triggerChecker; //Сам TriggerChecker
    
    //Другое 
    private bool isOnTrigger = false;
    private SpriteRenderer spriteRenderer;
    private InventoryManager inventoryManager;

    //Сама выдача предмета
    private void GiveItem()
    {
        if(giveCount > 0)
        {
            if (!giveRightAway)
                Instantiate(item, itemPos.position, Quaternion.identity, itemPos);
            else
            {
                GameObject newItem = Instantiate(item, itemPos.position, Quaternion.identity, itemPos);
                newItem.GetComponent<ItemInfo>().pickUp.Invoke();                    
            }
            giveCount--;
            if (giveCount <= 0)
                spriteRenderer.sprite = defaultSprite;
        }
    }

    //Основная логика
    private void Start() 
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(giveCount > 0)
        {
            if(checkFromTriggerChecker && triggerChecker.trigger)
                GiveItem();
            else if (isOnTrigger && Input.GetKeyDown(KeyCode.E))
                GiveItem();
        }
    }

    //Проверка триггеров
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!checkFromTriggerChecker && giveCount > 0 && collision.CompareTag(triggerTag))
        {
            isOnTrigger = true;
            spriteRenderer.sprite = triggerSprite;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!checkFromTriggerChecker && giveCount > 0 && collision.CompareTag(triggerTag))
        {
            isOnTrigger = false;
            spriteRenderer.sprite = defaultSprite;
        }
    }
}