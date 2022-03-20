using UnityEngine;

[CreateAssetMenu(fileName = "New MelleWeapon", menuName = "Items/MelleWeapons")]
public class MelleRangeWeapon : ScriptableObject
{
    //О предмете
    new public string name;
    [TextArea(3,3)] public string Dicription;
    public int Cost;
    [Tooltip("Attack Range")] public float attackRange;
    [Tooltip("Speed of attack")] public float attackRate;
    public int damage;
    float nextTime = 0f;
    public int ChanceOfDrop;
    
    //Другие переменные
    public Sprite sprite;
    public Sprite WhiteSprite;
    public Sprite[] extraSprites;

    public float GetNextTime()
    {
        return nextTime;
    }
    public void SetNextTime()
    {
        nextTime = Time.time + 1f / attackRate;
    }
}