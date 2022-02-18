using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private Item item;
    public Trader trader;

    [SerializeField]
    private GameObject objectItem;
    private ItemInGame itemInGame;
    
    [SerializeField]
    public Collider2D coll;

    [SerializeField]
    public SpriteRenderer sprite;
    [SerializeField]
    private SlotManager slot;
    private PlayerInventory inventory;
    public bool isOnTrigger;

    private void Start()
    {
        slot = FindObjectOfType<SlotManager>();
        inventory = FindObjectOfType<PlayerInventory>();
        itemInGame = GetComponent<ItemInGame>();
        item = GetComponent<ItemInGame>().item;
    }

    void OnTriggerStay2D(Collider2D oter)
    {
        if (oter.tag == "Player")
        {
            sprite.sprite = item.WhiteSprite;
            isOnTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            sprite.sprite = item.sprite;
            isOnTrigger = false;
        }
    }

    void Update()
    {
        if(isOnTrigger & !GameManager.isActiveAnyPanel)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (itemInGame.isForTrade)
                {
                    if (FindObjectOfType<GameManager>().playerCheese >= itemInGame.item.Cost)
                    {
                        FindObjectOfType<GameManager>().playerCheese -= itemInGame.item.Cost;
                        PickUpItem();
                    }
                    else trader.DisplayFraseNotEnoughMoney();
                }
                else
                    PickUpItem();
            }
        }
    }

    void PickUpItem()
    {
        if (item.isPassiveItem)
        {
            inventory.AddItem(item);
            // FindObjectOfType<GameManager>().items.Remove(objectItem.gameObject);
            coll.enabled = false;
            objectItem.SetActive(false);
            return;
        }
        if (item.CanRise == true)
        {
            slot.AddItem(item, objectItem);
            // FindObjectOfType<GameManager>().items.Remove(objectItem.gameObject);
            coll.enabled = false;
            objectItem.SetActive(false);
        }
        else
        {
            item.Action();
            // FindObjectOfType<GameManager>().items.Remove(objectItem.gameObject);
            coll.enabled = false;
        }
    }
}
