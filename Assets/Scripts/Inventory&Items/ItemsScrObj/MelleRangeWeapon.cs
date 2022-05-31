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
    public float effectTime;
    public int cost;
    public TypeOfAttack typeOfAttack;
    [Tooltip("Attack Range")] public float attackRange;
    [Tooltip("Speed of attack")] public float attackRate;
    public float lenght; //Длина атаки
    public int damage;
    public float stunTime = 0f;
    float nextTime = 0f;
    public int chanceOfDrop;

    //Другие переменные
    public Sprite sprite;
    public Sprite spriteInGame;
    public Sprite spriteInInventory;
    public Sprite whiteSprite;
    public Sprite[] extraSprites;
}