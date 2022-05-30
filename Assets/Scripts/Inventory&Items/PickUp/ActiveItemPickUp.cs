using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemPickUp : MonoBehaviour
{
    public ActiveItem activeItem; // Предмет
    public ItemInfo itemInfo; 
    private InventoryManager inventory; 
    private GameManager gameManager; 
    private InputManager inputManager;
    public Trader trader; 

    public bool canDestoring = true; // Будет ли уничтожаться при старте если предмет пустой
    private bool isWhiteSprite = false; // Стоит ли уже спрайт с обводкой

    private void Awake()
    {
        if(activeItem == null & canDestoring)
            Destroy(gameObject);
        else if(!canDestoring)// Если canDestroing == false, то просто спрайт прдмета будет становиться прозрачным
            itemInfo.SetActive(false);

        if(activeItem != null)
        {
            // Ставим спрайт предмета и ищем инвентарь
            activeItem.ActivateItem();
            gameObject.GetComponent<SpriteRenderer>().sprite = activeItem.sprite;
            itemInfo = GetComponent<ItemInfo>();
            
            //Записываем всю информацию о предмете в ItemInfo
            itemInfo.itemName = activeItem.name;
            itemInfo.discription = activeItem.dicription;
            itemInfo.cost = activeItem.cost;
            itemInfo.chanceOfDrop = activeItem.chanceOfDrop;
        }
        if(itemInfo.isForTrade & trader == null)
            itemInfo.isForTrade = false;
        
        inventory = FindObjectOfType<InventoryManager>();
        gameManager = FindObjectOfType<GameManager>();
        inputManager = FindObjectOfType<InputManager>();
    } 

    private void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.tag == "Player")
            itemInfo.isOnTrigger = true;
    }
    
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            itemInfo.isOnTrigger = false;
    }


    private void Update()
    {
        //Смена обычного спрайта на спрайт с обводкой и наоборот
        if(itemInfo.isOnTrigger & !isWhiteSprite)
            SetSpriteToWhite(true);
        else if(!itemInfo.isOnTrigger & isWhiteSprite)
            SetSpriteToWhite(false);
        
        //Поднимание прдмета
        if(itemInfo.isOnTrigger & Input.GetKeyDown(inputManager.PickUpButton))
        {
            if(!itemInfo.isForTrade)
                PickUp(); // Поднимаем предмет
            
            else trader.Trade(gameObject);
        }  
    }
    public void PickUp() //Поднятие предмета
    {
        inventory.AddActiveItem(activeItem, gameObject);
        if(!TryGetComponent(typeof(DontDestroyOnLoadNextScene), out Component comp))
            gameObject.AddComponent<DontDestroyOnLoadNextScene>();       
        
        itemInfo.SetActive(false);//Это НЕ ВЫКЛЮЧЕНИК ОБЪЕКТА
    }
    private void SetSpriteToWhite(bool white)
    {
        if(white) gameObject.GetComponent<SpriteRenderer>().sprite = activeItem.WhiteSprite;
        if(!white) gameObject.GetComponent<SpriteRenderer>().sprite = activeItem.sprite;
        
        isWhiteSprite = white;
    }
}
