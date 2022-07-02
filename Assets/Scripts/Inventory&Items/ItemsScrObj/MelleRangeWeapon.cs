using UnityEngine;


public enum TypeOfAttack
{
    Pinpoint,
    Wide,
    Above
}
[CreateAssetMenu(fileName = "New MelleWeapon", menuName = "Items/MelleWeapons")]
public class MelleRangeWeapon : ScriptableObject
{
    //О предмете
    new public string name;
    [TextArea(3,3)] public string dicription;
    public EffectsList effect;
    [Header("Еффект")]
    public EffectStats effectStats;
    public float effectTime;
    [Header("")]
    public int cost;
    public TypeOfAttack typeOfAttack;
    public float attackRange;
    public float attackRate;
    public float lenght; //Длина атаки
    public int damage;
    public float stunTime = 0f;
    public int chanceOfDrop;

    //Другие переменные
    public Sprite sprite;
    public Sprite spriteInGame;
    public Sprite spriteInInventory;
    public Sprite whiteSprite;
    public Sprite[] extraSprites;
}