using RimuruDev.Mechanics.Character;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New ActiveWeapon", menuName = "Items/ActiveItem")]
public class ActiveItem : ScriptableObject
{
    //О предмете
    new public string name;
    [TextArea(3, 3)]
    public string Dicription;
    public UnityEvent ItemAction;
    public int uses;
    public int Cost;
    public float useRate;
    public float waitTimeIfHave;
    [HideInInspector] public bool isItemCharged = false;
    public int ChanceOfDrop;

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
    private RatCharacterData plaeyrController;

    public void ActivateItem()
    {
        methods = FindObjectOfType<ActiveItemsMethods>();
        plaeyrController = FindObjectOfType<RatCharacterData>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        nextTime = Time.time;
        usesInGame = uses;
    }

    public void SetNextTime() { nextTime = Time.time + 1f / useRate; }
    public float GetNextTime() { return nextTime; }

    public void ChillyPepper()
    {
        int isDamaged = Random.Range(0, 2);

        if (isDamaged == 0)
            playerHealth.TakeHit(1);

        methods.SpawnFireball();
    }
}
