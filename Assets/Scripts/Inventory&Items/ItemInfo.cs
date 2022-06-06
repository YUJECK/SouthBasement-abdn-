using UnityEngine.Events;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public ItemClass itemClass; //Тип предмта
    public string itemName; //Имя
    public string discription; //Описание
    public int uses; //Количество использований  
    public bool active = true; //Может ли предмет еще исользоваться 
    public int cost; //Цена
    public int chanceOfDrop; //Шанс дропа
    public bool isOnTrigger; //Стоит ли игрок на предмете
    public bool isForTrade = false; // Продается ли этот предмет
    public UnityEvent pickUp; //Метод поднятия

    public int GetUses(){return uses;}
    public void SetActive(bool active)
    {
        GetComponent<SpriteRenderer>().enabled = active;
        GetComponent<Collider2D>().enabled = active;
    }
}
