using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Rigidbody2D player;
    public KeyCode button;
    public GameObject itemGameObject;
    public Item item;
    public Image sprite;
    public bool isItemActive;

    void Update()
    {       
        if(isItemActive)
        {
            item.SetSprite();

            if(Input.GetKeyDown(button))
                item.Action();

            if(item.GetUses() == 0)
            {
                Destroy(itemGameObject.gameObject);
                RemoveItem();
            }
        }
    }
    public void AddItem(Item newItem)
    {
        sprite.enabled = true;
        item = newItem;
        sprite.sprite = newItem.sprite;
        item.SetSprite();
        isItemActive = true;
    }
    public void RemoveItem()
    {
        sprite.enabled = false;
        item = null;
        sprite.sprite = null;
        isItemActive = false;
    }
    public void DropItem(GameObject item)
    {
        RemoveItem();
        item.SetActive(true);
        item.GetComponent<Collider2D>().enabled = true;
        item.transform.position = new Vector2(player.position.x + 1 / 6, player.position.y + 1 / 6);
    }
}
