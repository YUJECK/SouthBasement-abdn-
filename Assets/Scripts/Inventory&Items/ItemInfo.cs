using UnityEngine.Events;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public string itemName;
    public string itemTipe;
    public string discription;
    public int uses;
    public int cost;
    public int chanceOfDrop;
    public bool isOnTrigger;
    public UnityEvent pickUp;

    public int GetUses(){return uses;}
}
