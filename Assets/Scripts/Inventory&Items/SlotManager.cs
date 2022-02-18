using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public InventorySlot[] slots;
    public GameObject[] ActiveSlot;
    public Animator anim;
    public int UseSlot = 0;
    private void Start()
    {
        slots[UseSlot].button = KeyCode.Space; 
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(slots[UseSlot].itemGameObject != null)
                slots[UseSlot].DropItem(slots[UseSlot].itemGameObject);
        }
        if (!GameManager.isActiveAnyPanel)
        {   
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                UseSlot++;
                SetAnimation();

                if (UseSlot >= 3)
                    UseSlot = 0;

                for (int i = 0; i < slots.Length; i++)
                {
                    if (i != UseSlot)
                    {
                        slots[i].button = KeyCode.None;
                        ActiveSlot[i].SetActive(false);
                    }
                    else
                    {
                        slots[i].button = KeyCode.Space;
                        ActiveSlot[i].SetActive(true);
                    }
                }
            }
        }

    }
    public void AddItem(Item newItem, GameObject objectItem)
    {
        if (IsInventoryFull())
            slots[UseSlot].DropItem(slots[0].itemGameObject);

        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].isItemActive == false)
            {
                slots[i].AddItem(newItem);
                slots[i].itemGameObject = objectItem;
                return;
            }
        }
    }
    public bool IsInventoryFull()
    {
        int j = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isItemActive)
            {
                j++;
            }
        }
        if (j == slots.Length)
            return true;
        else
            return false;
    }
    private void SetAnimation()
    {
        anim.SetTrigger("ChangeSlot");
    }
}
