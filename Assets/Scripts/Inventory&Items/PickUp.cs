using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private MelleRangeWeapon weapon;
    public Trader trader;

    [SerializeField] private GameObject objectItem;
    private ItemInGame itemInGame;
    
    [SerializeField] public Collider2D coll;

    [SerializeField]
    public SpriteRenderer sprite;
    [SerializeField]
    private SlotManager slot;
    private RatAttack melleWeaponsList;
    private PlayerInventory inventory;
    public bool isOnTrigger;

    private void Start()
    {
        slot = FindObjectOfType<SlotManager>();
        inventory = FindObjectOfType<PlayerInventory>();
        itemInGame = GetComponent<ItemInGame>();
        melleWeaponsList = FindObjectOfType<RatAttack>();
        if(GetComponent<ItemInGame>().item != null)
            item = GetComponent<ItemInGame>().item;
        else if(GetComponent<ItemInGame>().weapon != null)
            weapon = GetComponent<ItemInGame>().weapon;
    }

    void OnTriggerStay2D(Collider2D oter)
    {
        if (oter.tag == "Player")
        {
            if(item != null)
                sprite.sprite = item.WhiteSprite;
            else if(weapon != null)
                sprite.sprite = weapon.WhiteSprite;
            isOnTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(item != null)
                sprite.sprite = item.sprite;
            else if(weapon != null)
                sprite.sprite = weapon.sprite;
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
                    if(itemInGame.item != null)
                    {
                        if (FindObjectOfType<GameManager>().playerCheese >= itemInGame.item.Cost)
                        {
                            FindObjectOfType<GameManager>().CheeseScore(itemInGame.item.Cost*-1);
                            PickUpItem(item);
                        }
                        else trader.DisplayFraseNotEnoughMoney();
                    }
                    else if(itemInGame.weapon != null)
                    {
                        if (FindObjectOfType<GameManager>().playerCheese >= itemInGame.weapon.Cost)
                        {
                            FindObjectOfType<GameManager>().CheeseScore(itemInGame.weapon.Cost*-1);
                            PickUpItem(null,weapon);
                        }
                        else trader.DisplayFraseNotEnoughMoney();
                    }
                    else
                        PickUpItem(item);
                }
                else if(weapon != null)
                    PickUpItem(null,weapon);
            }
        }
    }

    void PickUpItem(Item _item = null, MelleRangeWeapon _weapon = null)
    {
        if(item != null)
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
        else if(weapon != null)
        {
            melleWeaponsList.melleWeaponsList.Add(weapon);
            melleWeaponsList.SetMelleWeapon(weapon);
            coll.enabled = false;
            objectItem.SetActive(false);
        }
    }
}
