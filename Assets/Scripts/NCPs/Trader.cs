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
    
    
    public void DisplayFrase(string frase)
    {
        manager.DisplayText(frase);
    }

    private IEnumerator NotEnoghtMoney()
    {
        isTraderTalking = true;
        yield return new WaitForSeconds(3f);
        isTraderTalking = false;
    }
}