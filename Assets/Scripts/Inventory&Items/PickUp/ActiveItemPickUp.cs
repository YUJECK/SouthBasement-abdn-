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
    private bool isOnTrigger = false; // Если стоит на триггере
    private bool isWhiteSprite = false; // Стоит ли уже спрайт с обводкой
    public bool isForTrade = false; // Продается ли этот предмет


    private void Awake()
    {
        if(activeItem == null & canDestoring)
            Destroy(gameObject);

        else if(!canDestoring)// Если canDestroing == false, то просто спрайт прдмета будет становиться прозрачным
            gameObject.GetComponent<SpriteRenderer>().sprite = FindObjectOfType<GameManager>().hollowSprite;          

        if(activeItem != null)
        {
            // Ставим спрайт предмета и ищем инвентарь
            activeItem.ActivateItem();
            gameObject.GetComponent<SpriteRenderer>().sprite = activeItem.sprite;
            itemInfo = FindObjectOfType<ItemInfo>();
            
            //Записываем всю информацию о предмете в ItemInfo
            itemInfo.itemTipe = "ActiveItem";
            itemInfo.itemName = activeItem.name;
            itemInfo.discription = activeItem.Dicription;
            itemInfo.cost = activeItem.Cost;
            itemInfo.chanceOfDrop = activeItem.ChanceOfDrop;
        }
        if(isForTrade & trader == null)
            isForTrade = false;
        
        inventory = FindObjectOfType<InventoryManager>();
        gameManager = FindObjectOfType<GameManager>();
        inputManager = FindObjectOfType<InputManager>();
    } 

    private void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.tag == "Player")
        {
            isOnTrigger = true;
            itemInfo.isOnTrigger = isOnTrigger;
        }
    }
    
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            isOnTrigger = false;
            itemInfo.isOnTrigger = isOnTrigger;
        }
    }


    private void Update()
    {
        //Смена обычного спрайта на спрайт с обводкой и наоборот
        if(isOnTrigger & !isWhiteSprite)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = activeItem.WhiteSprite;
            isWhiteSprite = true;
        }
        else if(!isOnTrigger & isWhiteSprite)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = activeItem.sprite;
            isWhiteSprite = false;
        }


        //Поднимание прдмета
        if(isOnTrigger & Input.GetKeyDown(inputManager.PickUpButton))
        {
            if(isForTrade)
                Trade();

            else // Поднимаем предмет
            {
                inventory.AddActiveItem(activeItem, gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    private void Trade() // Продаем предмет
    {
        if(gameManager.playerCheese >= activeItem.Cost)
        {
            gameManager.CheeseScore(-activeItem.Cost);
            isForTrade = false;
            inventory.AddActiveItem(activeItem, gameObject);
            gameObject.SetActive(false);
        }
        else // Если не хватает сыра
            trader.DisplayFrase(trader.SentenceNotEnoghtCheese, 5f);
    }
}
