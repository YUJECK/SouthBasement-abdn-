using UnityEngine;

[CreateAssetMenu(fileName = "New MelleWeapon", menuName = "Items/MelleWeapons")]
public class MelleRangeWeapon : ScriptableObject
{
    //О предмете
    new public string name;
    [TextArea(3,3)] public string Dicription;
    public EffectsList effect;
    public float effectTime;
    public int Cost;
    [Tooltip("Attack Range")] public float attackRange;
    [Tooltip("Speed of attack")] public float attackRate;
    public float lenght; //Длина атаки
    public int damage;
    float nextTime = 0f;
    public int ChanceOfDrop;
    
    //Другие переменные
    public Sprite sprite;
    public Sprite spriteInInventory;
    public Sprite WhiteSprite;
    public Sprite[] extraSprites;
}