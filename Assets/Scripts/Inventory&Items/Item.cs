using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/UsebleItem")]
public class Item : ScriptableObject
{
    //О предмете
    new public string name;
    [TextArea(3,3)]
    public string Dicription;
    public int uses;
    private int usesInGame;
    public int Cost;
    public bool CanRise;
    public bool isPassiveItem;
    public float useRange;
    float nextTime = 0f;
    bool timeSetted = false;
    public int ChanceOfDrop;
    
    //Другие переменные
    public Weapons weapons;
    public Sprite sprite;
    public Sprite WhiteSprite;
    public Sprite[] extraSprites;

    public void Action()
    {
        if(name == "Энергетик")
        {
            FindObjectOfType<Health>().TakeAwayHealth(1,1);
            FindObjectOfType<Player>().speed++;
            usesInGame--;
        }
        if(name == "Стакан молока")
        {
            FindObjectOfType<Health>().SetBonusHealth(1,0);
            usesInGame--;
        }
        if(name == "Печенье")
        {
            if(FindObjectOfType<Health>().health != FindObjectOfType<Health>().maxHealth)
            {
                FindObjectOfType<Health>().Heal(1);
                FindObjectOfType<InventorySlot>().sprite.sprite = extraSprites[0];
                usesInGame--;
            }
        }
        if(name == "Черника")
        {
            if(FindObjectOfType<Health>().health != FindObjectOfType<Health>().maxHealth)
            {
                FindObjectOfType<Health>().Heal(1);
                usesInGame--;
            }
        }
        if(name == "Сырные палочки")
        {
            FindObjectOfType<Health>().TakeAwayHealth(1,1);
            FindObjectOfType<Health>().Heal(3);
            sprite = extraSprites[0];
            usesInGame--;
        }
    }
    public void PassiveEffect()
    {
        Debug.Log("PassiveEffectFunc1");
        weapons = FindObjectOfType<Weapons>();        

        if (name == "Чилли перец")
        {   
            Debug.Log("ChillyPepper");
            if(Time.time >= GetNextTime() || !timeSetted)
            {
                Debug.Log("ChillyPepper2");
                weapons.fireball.SpawnFireball();
                SetNextTime();
            }
        }   
    }
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
    public void SetSprite()
    {
        if(name == "Печенье")
        {
           if(usesInGame == 1)
                FindObjectOfType<InventorySlot>().sprite.sprite = extraSprites[0];
        }
    }
    public float GetNextTime()
    {
        return nextTime;
    }
    public void SetNextTime()
    {
        nextTime = Time.time + useRange;
        timeSetted = true;
    }
}