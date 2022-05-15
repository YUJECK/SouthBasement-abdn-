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
    [SerializeField]private bool isWhiteSprite = false; // Стоит ли уже спрайт с обводкой


    private void Awake() //"Создание" предмета
    {
        if(food == null && canDestoring)//Уничтожаем предмет если переменная предмета пустая
            Destroy(gameObject);
        else if(!canDestoring && food == null)// Если canDestroing == false, то просто спрайт прдмета будет становиться прозрачным
           itemInfo.SetActive(false);    
        
        if(food != null)
        {
            // Ставим спрайт предмета и ищем инвентарь
            food.ActiveItem();
            gameObject.GetComponent<SpriteRenderer>().sprite = food.sprite;
            itemInfo = GetComponent<ItemInfo>();
            
            //Записываем всю информацию о предмете в ItemInfo
            itemInfo.itemName = food.name;
            itemInfo.discription = food.Dicription;
            itemInfo.uses = food.uses;
            itemInfo.cost = food.Cost;
            itemInfo.chanceOfDrop = food.ChanceOfDrop;
        }
        if(itemInfo.isForTrade & trader == null)
            itemInfo.isForTrade = false;

        inventory = FindObjectOfType<InventoryManager>();
        gameManager = FindObjectOfType<GameManager>();
        inputManager = FindObjectOfType<InputManager>();
    } 

    //Определение стоит ли игрок на прдемете или нет
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
        inventory.AddFood(food, gameObject);
        if(!TryGetComponent(typeof(DontDestroyOnLoadNextScene), out Component comp))
            gameObject.AddComponent<DontDestroyOnLoadNextScene>();       
        
        itemInfo.SetActive(false);//Это НЕ ВЫКЛЮЧЕНИК ОБЪЕКТА
    }
    private void SetSpriteToWhite(bool white)
    {
        if(white) gameObject.GetComponent<SpriteRenderer>().sprite = food.WhiteSprite;
        if(!white) gameObject.GetComponent<SpriteRenderer>().sprite = food.sprite;
        
        isWhiteSprite = white;
    }
}
