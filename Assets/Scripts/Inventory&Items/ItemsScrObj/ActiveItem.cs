using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    private PlayerHealth playerHealth;
    private PlayerController plaeyrController;

    public void ActivateItem()
    {
        methods = FindObjectOfType<ActiveItemsMethods>();
        plaeyrController = FindObjectOfType<PlayerController>();
        playerHealth =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        isItemCharged = false;
        nextTime = Time.time;
        usesInGame = uses;
    }

    public void SetNextTime(){nextTime = Time.time + useRate;}
    public float GetNextTime(){return nextTime;}

    public void ChillyPepper()
    {
        int isDamaged = Random.Range(0,7);

        if(isDamaged == 0)
            playerHealth.TakeHit(10);

        methods.SpawnFireball();    
    }
    public void Mousetrap() {  methods.SpawnMousetrap();
        FindObjectOfType<RatConsole>().DisplayText("Мышеловка", Color.white, RatConsole.Mode.ConsoleMessege, "<ActiveItem.cs>");
        SetSprite(extraSprites[0], slot.objectOfItem.GetComponent<SpriteRenderer>(), slot.slotIcon);
        slot.itemInfo.active = false;
    }
    public void SetSprite(Sprite newSprite, SpriteRenderer spriteRend = null, Image image = null)
    {
        if (spriteRend != null)
            spriteRend.sprite = newSprite;

        if (image != null)
            image.sprite = newSprite;
    }
}
