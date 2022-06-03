using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleWeaponPickUp : MonoBehaviour
{
    public MelleRangeWeapon melleWeapon; // Предмет
    public ItemInfo itemInfo;
    private InventoryManager inventory; 
    private GameManager gameManager; 
    public Trader trader; 
    private InputManager inputManager;

    public bool canDestoring = true; // Будет ли уничтожаться при старте если предмет пустой
    private bool isWhiteSprite = false; // Стоит ли уже спрайт с обводкой

    private void Awake()
    {
        if(melleWeapon == null && canDestoring)
            Destroy(gameObject);
        else if(!canDestoring && melleWeapon == null) // Если canDestroing == false, то просто спрайт прдмета будет становиться прозрачным
            itemInfo.SetActive(false);      
        
        if(melleWeapon != null)
        {
            // Ставим спрайт предмета и ищем инвентарь
            gameObject.GetComponent<SpriteRenderer>().sprite = melleWeapon.spriteInGame;
            itemInfo = GetComponent<ItemInfo>();

            //Записываем всю информацию о предмете в ItemInfo
            itemInfo.itemName = melleWeapon.name;
            itemInfo.discription = melleWeapon.dicription;
            itemInfo.cost = melleWeapon.cost;
            itemInfo.chanceOfDrop = melleWeapon.chanceOfDrop;
        }
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

        if(itemInfo.isForTrade && trader == null)
            itemInfo.isForTrade = false;
    }
    public void PickUp()
    {
        inventory.AddMelleWeapon(melleWeapon, gameObject);
        if(!TryGetComponent(typeof(DontDestroyOnLoadNextScene), out Component comp))
            gameObject.AddComponent<DontDestroyOnLoadNextScene>();       
        
        itemInfo.SetActive(false);//Это НЕ ВЫКЛЮЧЕНИК ОБЪЕКТА
    }
    private void SetSpriteToWhite(bool white)
    {
        if(white) gameObject.GetComponent<SpriteRenderer>().sprite = melleWeapon.whiteSprite;
        if(!white) gameObject.GetComponent<SpriteRenderer>().sprite = melleWeapon.spriteInGame;
        
        isWhiteSprite = white;
    }
}