using UnityEngine;

public class FoodPickUp : MonoBehaviour
{
    public FoodItem food; // Предмет
    private InventoryManager inventory; 
    private GameManager gameManager; 
    [SerializeField] private Trader trader; 

    private bool isOnTrigger = false; // Если стоит на триггере
    private bool isWhiteSprite = false; // Стоит ли уже спрайт с обводкой
    [SerializeField] private bool isForTrade = false; // Продается ли этот предмет


    private void Awake()
    {
        // Ставим спрайт предмета и ищем инвентарь
        gameObject.GetComponent<SpriteRenderer>().sprite = food.sprite;
        inventory = FindObjectOfType<InventoryManager>();
        gameManager = FindObjectOfType<GameManager>();
    } 

    private void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.tag == "Player")
            isOnTrigger = true;
    }
    
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            isOnTrigger = false;
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
        if(isOnTrigger & Input.GetKeyDown(KeyCode.E))
        {
            if(isForTrade)
                Trade();

            else // Поднимаем предмет
            {
                inventory.AddFood(food, gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    private void Trade() // Продаем предмет
    {
        if(gameManager.playerCheese >= food.Cost)
        {
            gameManager.playerCheese -= food.Cost;
            isForTrade = false;
            inventory.AddFood(food, gameObject);
            gameObject.SetActive(false);
        }
        else // Если не хватает сыра
            trader.StartCoroutine("NotEnoghtMoney");
    }
}
