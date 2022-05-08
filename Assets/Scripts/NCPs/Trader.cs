using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    private GameManager gameManager;
    bool isOnTrigger;
    public DialogueManager manager;
    
    [Header("ItemSlots")]
    public int boxesForItems = 3;
    public Transform[] itemsTransform;
    public List<Item> items;
    public List<int> itemsIndex;

    [Header("Sentences")]
    public bool isTraderTalking = false;
    [TextArea(1,3)] public string SentenceNotEnoghtCheese;
    
    [System.Serializable]public struct Item
    {
        public bool isSold; //Продан ли этот предмет

        public GameObject item; //Сам объект
        public ItemInfo itemInfo;//АйтемИнфо этого объекта
        public GameObject itemPrefab;//Оригинальный префаб предмета, нужен для ремува предмета из листа
    
        public Item(GameObject item, ItemInfo itemInfo, GameObject itemPrefab)
        {
            this.item = item;
            this.itemInfo = itemInfo;
            this.itemPrefab = itemPrefab;
            isSold = false;
        }
    };
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        //Спавн предметов
        SpawnItem(itemsTransform[0].position);
        SpawnItem(itemsTransform[1].position);
        SpawnItem(itemsTransform[2].position);

        //Делаем чтобы по началу была такая фраза
        manager.DisplayText("Заходи, товары по низким ценам!");
    }
    
    private void Update()
    {
        if(!isTraderTalking) //Выводим инфу предмета когда к нему подходит игрок
        {
            if(!items[0].isSold && items[0].itemInfo.isOnTrigger)
                DisplayItemInfo(items[0].itemInfo);

            if(!items[0].isSold && items[1].itemInfo.isOnTrigger)
                DisplayItemInfo(items[1].itemInfo);
                
            if(!items[0].isSold && items[2].itemInfo.isOnTrigger)
                DisplayItemInfo(items[2].itemInfo); 
        }
    }

    private void SpawnItem(Vector3 pos)
    {
        int tmp; //Идекс предметов 

        //Спавн предмета
        tmp = Random.Range(0,gameManager.items.Count);
        GameObject item = Instantiate(gameManager.items[tmp], pos, Quaternion.identity, itemsTransform[0]);
        
        //Удаление их листа предметов и запись его в лист для торговли
        gameManager.traderItems.Add(gameManager.items[tmp]);
        gameManager.items.Remove(gameManager.items[tmp]);
        
        items.Add(new Item(item, item.GetComponent<ItemInfo>(), 
        gameManager.traderItems[gameManager.traderItems.Count-1]));
        SetItemForTrade(item);
    }

    private void SetItemForTrade(GameObject item)
    {
        if(item.TryGetComponent(out FoodPickUp food))
        {
            item.GetComponent<ItemInfo>().isForTrade = true;
            food.trader = GetComponent<Trader>();
            item.GetComponent<Collider2D>().offset = new Vector2(0,-0.5f);
            item.GetComponent<BoxCollider2D>().size = new Vector2(0.1f,0.3f);
            return;
        }
        if(item.TryGetComponent(out ActiveItemPickUp activeItem))
        {
            item.GetComponent<ItemInfo>().isForTrade = true;
            activeItem.trader = GetComponent<Trader>();
            item.GetComponent<Collider2D>().offset = new Vector2(0,-0.5f);
            item.GetComponent<BoxCollider2D>().size = new Vector2(0.1f,0.3f);
            return;
        }
        if(item.TryGetComponent(out MelleWeaponPickUp melleWeapon))
        {
            item.GetComponent<ItemInfo>().isForTrade = true;
            melleWeapon.trader = GetComponent<Trader>();
            item.GetComponent<Collider2D>().offset = new Vector2(0,-0.5f);
            item.GetComponent<BoxCollider2D>().size = new Vector2(0.1f,0.3f);
            return;
        }
    }

    public void Trade(GameObject obj) // Продаем предмет
    {
        Item item = new Item(); //Сам предмет

        foreach(Item _item in items)//Поиск этого предмета по объекту
        {
            if(obj == _item.item)
                item = _item;
        }

        //Сам процесс продажи предмета
        if(gameManager.playerCheese >= item.itemInfo.cost)
        {
            gameManager.CheeseScore(-item.itemInfo.cost);
            gameManager.traderItems.Remove(item.itemPrefab);
            item.itemInfo.pickUp.Invoke();
            item.isSold = true;
            item.itemInfo.isForTrade = false;
        }
        else // Если не хватает сыра
            DisplayFrase(SentenceNotEnoghtCheese, 5f);
    }

    public void DisplayFrase(string frase, float time){StartCoroutine(displayFrase(frase,time));} //Торговец что то говорит
    
    //Показывает информацию о педмете
    public void DisplayItemInfo(ItemInfo itemInfo)
    {
        manager.DisplayText(
        "Это " + itemInfo.itemName + ". " +
        itemInfo.discription + ". " + 
        "Стоит " + itemInfo.cost + " сыра");
    }
    
    //Корутина для того чтобы текст оставался на определенное время
    private IEnumerator displayFrase(string frase, float time)
    {
        isTraderTalking = true;
        manager.DisplayText(frase);
        yield return new WaitForSeconds(time);
        isTraderTalking = false;
    }
}