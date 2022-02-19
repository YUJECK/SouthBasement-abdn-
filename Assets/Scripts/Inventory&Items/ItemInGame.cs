using UnityEngine;
using UnityEngine.UI;

public class ItemInGame : MonoBehaviour
{
    public Item item;
    public SpriteRenderer sprite;
    public bool isForTrade;

    void Start()
    {
        ActiveItem();
    }

    public void ActiveItem()
    {
        sprite.sprite    = item.sprite;
        if (!item.isPassiveItem) { item.ActiveUses(); }
    }

    void Update()
    {
        if(!item.isPassiveItem & item.GetUses() <= 0)
            Destroy(gameObject);
    }
}
