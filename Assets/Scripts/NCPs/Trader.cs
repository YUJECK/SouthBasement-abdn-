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
    public Transform item1Transform;
    public Transform item2Transform;
    public Transform item3Transform;
    public List<GameObject> itemsForTrade;

    [Header("Sentences")]
    public bool isTraderTalking = false;
    [TextArea(1,3)]
    public string SentenceNotEnoghtCheese;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        GameObject item_1 = gameManager.items[Random.Range(0,gameManager.items.Count)];
        gameManager.items.Remove(item_1);
        SetItemForTrade(item_1);
        Instantiate(item_1,item1Transform.position,Quaternion.identity,item1Transform);

        GameObject item_2 = gameManager.items[Random.Range(0,gameManager.items.Count)];
        gameManager.items.Remove(item_2);
        SetItemForTrade(item_2);
        Instantiate(item_2,item2Transform.position,Quaternion.identity,item2Transform);

        GameObject item_3 = gameManager.items[Random.Range(0,gameManager.items.Count)];
        gameManager.items.Remove(item_3);
        SetItemForTrade(item_3);
        Instantiate(item_3,item3Transform.position,Quaternion.identity,item3Transform);
    }
    
    private void Update()
    {
        if(!isTraderTalking)
        {
            if(item1Transform.GetChild(0).GetComponent<ItemInfo>().isOnTrigger)
                DisplayItemInfo(item1Transform.GetChild(0).gameObject);
            if(item2Transform.GetChild(0).GetComponent<ItemInfo>().isOnTrigger)
                DisplayItemInfo(item2Transform.GetChild(0).gameObject);
            if(item3Transform.GetChild(0).GetComponent<ItemInfo>().isOnTrigger)
                DisplayItemInfo(item3Transform.GetChild(0).gameObject);
        }
    }

    private void SetItemForTrade(GameObject item)
    {
        if(item.TryGetComponent(out FoodPickUp food))
        {
            food.isForTrade = true;
            food.trader = GetComponent<Trader>();
            item.GetComponent<Collider2D>().offset = new Vector2(0,-0.5f);
            return;
        }
        if(item.TryGetComponent(out ActiveItemPickUp activeItem))
        {
            activeItem.isForTrade = true;
            activeItem.trader = GetComponent<Trader>();
            item.GetComponent<Collider2D>().offset = new Vector2(0,-0.5f);
            return;
        }
        if(item.TryGetComponent(out MelleWeaponPickUp melleWeapon))
        {
            melleWeapon.isForTrade = true;
            melleWeapon.trader = GetComponent<Trader>();
            item.GetComponent<Collider2D>().offset = new Vector2(0,-0.5f);
            return;
        }
    }
    
    //Торговец что то говорит
    public void DisplayFrase(string frase, float time)
    {
        StartCoroutine(displayFrase(frase,time));
    }
    
    //Показывает информацию о педмете
    public void DisplayItemInfo(GameObject item)
    {
        manager.DisplayText(
        "Это " + item.GetComponent<ItemInfo>().itemName + " " +
        item.GetComponent<ItemInfo>().discription + 
        "Стоит " + item.GetComponent<ItemInfo>().cost + "сыра");
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