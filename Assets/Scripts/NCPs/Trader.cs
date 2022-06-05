using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trader : MonoBehaviour
{
    private GameManager gameManager;
    bool isOnTrigger;
    public DialogueManager manager;
    public List<ItemClass> itemsClassesToTrade;
    
    [Header("ItemSlots")]
    public int boxesForItems = 3;
    public Transform[] itemsTransform;
    public Text[] itemsPrice;
    public List<Item> items;

    [Header("Sentences")]
    public bool isTraderTalking = false;
    [TextArea(1,3)] public string SentenceNotEnoghtCheese;
    
    [System.Serializable]public class Item
    {
        public bool isSold; //Продан ли этот предмет

        public GameObject item; //Сам объект
        public ItemInfo itemInfo;//АйтемИнфо этого объекта
        public GameObject itemPrefab;//Оригинальный префаб предмета, нужен для ремува предмета из листа
        public Text price;

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
        for(int i = 0; i < itemsTransform.Length; i++)
            SpawnItem(itemsTransform[i].position, itemsPrice[i]);

        //Делаем чтобы по началу была такая фраза
        manager.DisplayText("Заходи, товары по низким ценам!");
    }
    
    private void Update()
    {
        if(!isTraderTalking) //Выводим инфу предмета когда к нему подходит игрок
        {
            for(int i = 0; i < items.Count; i++)
            if(!items[i].isSold && items[i].itemInfo.isOnTrigger)
                DisplayItemInfo(items[i].itemInfo); 
        }
    }

    private void SpawnItem(Vector3 pos, Text itemPriceText)
    {
        List<GameObject> allItems = new List<GameObject>();

        //Собираем все предметы в один лист
        
        if(itemsClassesToTrade.Contains(ItemClass.Food))
        {
            for (int i = 0; i < gameManager.Food.Count; i++)
                allItems.Add(gameManager.Food[i]);
        }
        if (itemsClassesToTrade.Contains(ItemClass.MelleRangeWeapon))
        {
            for (int i = 0; i < gameManager.MelleRange.Count; i++)
                allItems.Add(gameManager.MelleRange[i]);
        }
        if (itemsClassesToTrade.Contains(ItemClass.ActiveItem))
        {
            for (int i = 0; i < gameManager.ActiveItems.Count; i++)
                allItems.Add(gameManager.ActiveItems[i]);
        }
        if (itemsClassesToTrade.Contains(ItemClass.PassiveItem))
        {
            for (int i = 0; i < gameManager.PassiveItems.Count; i++)
                allItems.Add(gameManager.PassiveItems[i]);
        }
        int tmp; //Идекс предметов 

        //Спавн предмета
        tmp = Random.Range(0, allItems.Count);
        GameObject item = Instantiate(allItems[tmp], pos, Quaternion.identity, itemsTransform[0]);
        
        //Запись его в лист для торговли
        gameManager.traderItems.Add(allItems[tmp]);
        
        items.Add(new Item(item, item.GetComponent<ItemInfo>(), 
        gameManager.traderItems[gameManager.traderItems.Count-1]));
        items[items.Count-1].price = itemPriceText;

        itemPriceText.text = item.GetComponent<ItemInfo>().cost.ToString();
        SetItemForTrade(item);

        //Удаление иp листа предметов
        switch (items[items.Count-1].itemInfo.itemClass)
        {
            case ItemClass.Food:
                for (int i = 0; i < gameManager.Food.Count; i++)
                    if (gameManager.Food[i] == allItems[tmp]) gameManager.Food.RemoveAt(i);
                break;
            
            case ItemClass.MelleRangeWeapon:
                for (int i = 0; i < gameManager.MelleRange.Count; i++)
                    if (gameManager.MelleRange[i] == allItems[tmp]) gameManager.MelleRange.RemoveAt(i);
                break;
            case ItemClass.ActiveItem:
                for (int i = 0; i < gameManager.ActiveItems.Count; i++)
                    if (gameManager.ActiveItems[i] == allItems[tmp]) gameManager.ActiveItems.RemoveAt(i);
                break;
            case ItemClass.PassiveItem:
                for (int i = 0; i < gameManager.PassiveItems.Count; i++)
                    if (gameManager.PassiveItems[i] == allItems[tmp]) gameManager.PassiveItems.RemoveAt(i);
                break;
        }
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
        Item item = new Item(null, null, null); //Сам предмет

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
            item.price.gameObject.SetActive(false);

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
        itemInfo.discription + ".");
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