using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public enum UseMode
{
    Charge,
    UseImmedialty
}

[CreateAssetMenu(fileName = "New ActiveWeapon", menuName = "Items/ActiveItem")]
public class ActiveItem : ScriptableObject
{
    public UseMode useMode;
    //О предмете
    new public string name;
    [TextArea(3,6)]
    public string dicription;
    public UnityEvent itemAction;
    public float useRate;
    public int uses;
    public int chanceOfDrop;
    public int cost;
    public float chargeTime;
    [HideInInspector] public bool isItemCharged = false;
    
    //Внутренние поля
    private int usesInGame;
    private float nextTime;

    //Другие переменные
    public Sprite sprite;
    public Sprite WhiteSprite;
    public Sprite[] extraSprites;

    //Ссылки на другие скрипты
    [HideInInspector] public ActiveItemsMethods methods;
    [HideInInspector] public ActiveItemsSlots slot;
    private Health playerHealth;
    private Player plaeyrController;

    public void ActivateItem()
    {
        methods = FindObjectOfType<ActiveItemsMethods>();
        plaeyrController = FindObjectOfType<Player>();
        playerHealth =  GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        isItemCharged = false;
        nextTime = Time.time;
        usesInGame = uses;
    }

    public void SetNextTime(){nextTime = Time.time + 1f / useRate;}
    public float GetNextTime(){return nextTime;}

    public void ChillyPepper()
    {
        int isDamaged = Random.Range(0,2);

        if(isDamaged == 0)
            playerHealth.TakeHit(10);

        methods.SpawnFireball();    
    }
}
