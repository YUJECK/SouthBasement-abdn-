using UnityEngine;
using UnityEngine.UI;

public class ItemInGame : MonoBehaviour
{
    public Item item;
    public MelleRangeWeapon weapon;
    public SpriteRenderer sprite;
    public bool isForTrade;

    void Start()
    {
        if(item != null)
            ActiveItem(item);
            
        if(weapon != null)
            ActiveItem(null,weapon);
    }

    public void ActiveItem(Item _item = null, MelleRangeWeapon _weapon = null)
    {
        if(item != null)
        {
            sprite.sprite = item.sprite;
            if (!item.isPassiveItem) { item.ActiveUses(); }
        }
        else if(weapon != null)
            sprite.sprite = weapon.sprite;
    }

    void Update()
    {
        if(item != null)
        {
            if(!item.isPassiveItem & item.GetUses() <= 0)
                Destroy(gameObject);
        }
    }
}
