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
    public GameObject[] items;
    public List<int> itemsIndex;

    [Header("Sentences")]
    public bool isTraderTalking = false;
    [TextArea(1,3)] public string SentenceNotEnoghtCheese;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        int tmp;
        //Спавним предметы
        tmp = Random.Range(0,gameManager.items.Count);
        GameObject item_1 = Instantiate(gameManager.items[tmp],itemsTransform[0].position,Quaternion.identity, itemsTransform[0]);
        gameManager.traderItems.Add(gameManager.items[tmp]);
        itemsIndex.Add(gameManager.traderItems.Count-1);
        gameManager.items.Remove(gameManager.items[tmp]);
        items[0] = item_1;
        SetItemForTrade(item_1);

        tmp = Random.Range(0,gameManager.items.Count);
        GameObject item_2 = Instantiate(gameManager.items[tmp],itemsTransform[1].position,Quaternion.identity, itemsTransform[1]);
        gameManager.traderItems.Add(gameManager.items[tmp]);
        gameManager.items.Remove(gameManager.items[tmp]);
        items[1] = item_2;
        SetItemForTrade(item_2);

        tmp = Random.Range(0,gameManager.items.Count);
        GameObject item_3 = Instantiate(gameManager.items[tmp],itemsTransform[2].position,Quaternion.identity, itemsTransform[2]);
        gameManager.traderItems.Add(gameManager.items[tmp]);
        gameManager.items.Remove(gameManager.items[tmp]);
        items[2] = item_3;
        SetItemForTrade(item_3);

        //Делаем чтобы по началу была такая фраза
        manager.DisplayText("Заходи, товары по низким ценам!");
    }
    
    private void Update()
    {
        if(!isTraderTalking) //Выводим инфу предмета когда к нему подходит игрок
        {
            if(items[0].GetComponent<ItemInfo>().isOnTrigger)
                DisplayItemInfo(items[0]);

            if(items[1].GetComponent<ItemInfo>().isOnTrigger)
                DisplayItemInfo(items[1]);
                
            if(items[2].GetComponent<ItemInfo>().isOnTrigger)
                DisplayItemInfo(items[2]); 
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

    public void Trade(GameObject item) // Продаем предмет
    {
        if(gameManager.playerCheese >= item.GetComponentInChildren<ItemInfo>().cost)
        {
            gameManager.CheeseScore(-item.GetComponentInChildren<ItemInfo>().cost);
            gameManager.traderItems.Remove(item.GetComponentInChildren<Transform>().gameObject);
            item.GetComponentInChildren<ItemInfo>().pickUp.Invoke();
        }
        else // Если не хватает сыра
            DisplayFrase(SentenceNotEnoghtCheese, 5f);
    }

    public void DisplayFrase(string frase, float time){StartCoroutine(displayFrase(frase,time));} //Торговец что то говорит
    
    //Показывает информацию о педмете
    public void DisplayItemInfo(GameObject item)
    {
        manager.DisplayText(
        "Это " + item.GetComponent<ItemInfo>().itemName + ". " +
        item.GetComponent<ItemInfo>().discription + ". " + 
        "Стоит " + item.GetComponent<ItemInfo>().cost + " сыра");
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