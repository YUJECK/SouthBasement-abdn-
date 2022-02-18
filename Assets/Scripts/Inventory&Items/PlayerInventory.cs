using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Image spriteWeapon;
    public Player player;
    public void AddItem(Item item)
    {
        player.weapons[0] = item; 
        spriteWeapon.sprite = item.sprite;
    }
}
