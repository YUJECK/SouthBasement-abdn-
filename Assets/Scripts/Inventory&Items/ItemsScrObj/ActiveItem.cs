using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActiveWeapon", menuName = "Items/ActiveItem")]
public class ActiveItem : ScriptableObject
{
    //О предмете
    new public string name;
    [TextArea(3,3)]
    public string Dicription;
    public int uses;
    private int usesInGame;
    public int Cost;
    public float useRange;
    private float nextTime = 0f;
    public int ChanceOfDrop;
    
    //Другие переменные
    public Weapons weapons;
    public Sprite sprite;
    public Sprite WhiteSprite;
    public Sprite[] extraSprites;


    public void Action()
    {
        if(name == "Чилли перец")
        {
            
        }
    }
}
