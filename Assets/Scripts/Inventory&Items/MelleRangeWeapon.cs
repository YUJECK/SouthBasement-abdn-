using UnityEngine;

[CreateAssetMenu(fileName = "New MelleWeapon", menuName = "Items/Weapons")]
public class MelleRangeWeapon : ScriptableObject
{
    //О предмете
    new public string name;
    [TextArea(3,3)]
    public string Dicription;
    public int uses = 10;
    private int usesInGame;
    public int Cost;
    public float useRange;
    public float attackRange;
    public int damage;
    float nextTime = 0f;
    public int ChanceOfDrop;
    
    //Другие переменные
    public Weapons weapons;
    public Sprite sprite;
    public Sprite WhiteSprite;
    public Sprite[] extraSprites;
    public void ActiveUses()
    {
        usesInGame = uses;
    }
    public void SetUses(bool PlusUses,int BonusUses)
    {
        if(PlusUses)
            usesInGame += BonusUses;    
        else
            usesInGame -= BonusUses;
    }
    public int GetUses()
    {
        return usesInGame;
    }
    public float GetNextTime()
    {
        return nextTime;
    }
    public void SetNextTime()
    {
        nextTime = Time.time + useRange;
    }
}