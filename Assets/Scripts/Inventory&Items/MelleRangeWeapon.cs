using UnityEngine;

[CreateAssetMenu(fileName = "New MelleWeapon", menuName = "Items/Weapons")]
public class MelleRangeWeapon : ScriptableObject
{
    //О предмете
    new public string name;
    [TextArea(3,3)] public string Dicription;
    public int Cost;
    [Tooltip("Attack Range")] public float useRange;
    [Tooltip("Speed of attack")] public float attackRate;
    public int damage;
    float nextTime = 0f;
    public int ChanceOfDrop;
    
    //Другие переменные
    public Weapons weapons;
    public Sprite sprite;
    public Sprite WhiteSprite;
    public Sprite[] extraSprites;

    public float GetNextTime()
    {
        return nextTime;
    }
    public void SetNextTime()
    {
        nextTime = Time.time + useRange;
    }
}