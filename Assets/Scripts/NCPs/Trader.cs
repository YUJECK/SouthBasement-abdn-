using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    GameManager GameManager;
    bool isOnTrigger;
    public DialogueManager manager;
    
    [Header("ItemSlots")]
    public int boxesForItems = 3;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    //public GameObject item4;
    //public GameObject item5;

    [Header("Sentences")]
    public bool isTraderTalking = false;
    [TextArea(1,3)]
    public string SentenceNotEnoghtMoney;
    [TextArea(1, 3)]
    public string FirstMeetingTraderSentence;

    public List<GameObject> itemsForTrade;

    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        
        for (int i = 0; i < boxesForItems; i++)
        {
            int itemIndex = Random.Range(0,GameManager.items.Count);
            itemsForTrade.Add(GameManager.items[itemIndex]);
            GameManager.items.Remove(GameManager.items[itemIndex]);
        }
        ActivateItem(item1, 0); 
        ActivateItem(item2, 1); 
        ActivateItem(item3, 2); 
    }

    private void Update()
    {
        if (item1 != null)
            DisplayItemInfo(item1);
        if (item2 != null)
            DisplayItemInfo(item2);
        if (item3 != null)
            DisplayItemInfo(item3);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
            manager.DisplayText(FirstMeetingTraderSentence);
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
            isOnTrigger = false;
    }

    public void DisplayFraseNotEnoughMoney()
    {
        manager.DisplayText(SentenceNotEnoghtMoney);
        StartCoroutine("NotEnoghtMoney");
    }
    private void DisplayItemInfo(GameObject item)
    {
        if (item.GetComponent<PickUp>().isOnTrigger & !isTraderTalking)
        {
            //Eto,Ono,Stoit,Sira 
            manager.DisplayText("Это "+item.GetComponent<ItemInGame>().item.name
            + ". " + "Он "+ item.GetComponent<ItemInGame>().item.Dicription+". "+"Стоит " 
            + item.GetComponent<ItemInGame>().item.Cost.ToString() + " Сыра");
        }
    }
    private void ActivateItem(GameObject item, int itemIndex)
    {
        item.GetComponent<ItemInGame>().item = itemsForTrade[itemIndex].GetComponent<ItemInGame>().item;
        item.GetComponent<ItemInGame>().ActiveItem();
    }

    private IEnumerator NotEnoghtMoney()
    {
        isTraderTalking = true;
        yield return new WaitForSeconds(3f);
        isTraderTalking = false;
    }
}