using UnityEngine;

public class FoodPickUp : MonoBehaviour
{
    public FoodItem food; // Предмет
    public ItemInfo itemInfo;
    private InventoryManager inventory; 
    private GameManager gameManager; 
    public Trader trader; 
    public InputManager inputManager;
    public bool canDestoring = true; // Будет ли уничтожаться при старте если предмет пустой
    private bool isOnTrigger = false; // Если стоит на триггере
    private bool isWhiteSprite = false; // Стоит ли уже спрайт с обводкой
    public bool isForTrade = false; // Продается ли этот предмет


    private void Awake() //"Создание" предмета
    {
        if(food == null & canDestoring)//Уничтожаем предмет если переменная предмета пустая
            Destroy(gameObject);

        else if(!canDestoring)// Если canDestroing == false, то просто спрайт прдмета будет становиться прозрачным
            gameObject.GetComponent<SpriteRenderer>().sprite = FindObjectOfType<GameManager>().hollowSprite;      
        
        if(food != null)
        {
            // Ставим спрайт предмета и ищем инвентарь
            food.ActiveItem();
            gameObject.GetComponent<SpriteRenderer>().sprite = food.sprite;
            itemInfo = FindObjectOfType<ItemInfo>();
            
            //Записываем всю информацию о предмете в ItemInfo
            itemInfo.itemTipe = "Food";
            itemInfo.itemName = food.name;
            itemInfo.itemName = food.name;
            itemInfo.uses = food.uses;
            itemInfo.cost = food.Cost;
            itemInfo.chanceOfDrop = food.ChanceOfDrop;
        }
        if(isForTrade & trader == null)
            isForTrade = false;

        inventory = FindObjectOfType<InventoryManager>();
        gameManager = FindObjectOfType<GameManager>();
        inputManager = FindObjectOfType<InputManager>();
    } 

    //Определение стоит ли игрок на прдемете или нет
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
            gameObject.GetComponent<SpriteRenderer>().sprite = food.WhiteSprite;
            isWhiteSprite = true;
        }
        else if(!isOnTrigger & isWhiteSprite)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = food.sprite;
            isWhiteSprite = false;
        }

        //Поднимание прдмета
        if(isOnTrigger & Input.GetKeyDown(inputManager.PickUpButton))
        {
            // Поднимаем предмет
            PickUp();
        }
    }

    public void PickUp() //Поднятие предмета
    {
        inventory.AddFood(food, gameObject);
        gameObject.SetActive(false);
    }
}
