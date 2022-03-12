using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<FoodSlots> foodItems; // Слоты для еды
    public int activeFoodSlot; // Номер активного слота для игры

    [Header("")]
    public List<ActiveItemsSlots> activeItems; // Слоты для активок
    public int activeAciveItemSlot; // Номер активного слота для игры
    
    [Header("")]
    public List<PassiveItemsSlots> passiveItems; // Слоты для пассивок

    public void AddFood(FoodItem newFood) // Добавление еды в инвентарь
    {
        if(foodItems[activeFoodSlot].isEmpty)
            foodItems[activeFoodSlot].Add(newFood);
        else
        {
            foodItems[activeFoodSlot].Drop();
            foodItems[activeFoodSlot].Add(newFood);
        }
    }
    public void AddActiveItem(ActiveItem newActiveItem) // Добавление активки в инвентарь
    {
        if(activeItems[activeAciveItemSlot].isEmpty)
            activeItems[activeAciveItemSlot].Add(newActiveItem);
        else
        {
            activeItems[activeAciveItemSlot].Drop();
            activeItems[activeAciveItemSlot].Add(newActiveItem);
        }
    }

    //Удаление из инвенторя

    public void RemoveFood(bool isDrop, int slotIndex) // Удаление еды в инвентарь
    {
        if(!foodItems[slotIndex].isEmpty)
        {
            foodItems[activeFoodSlot].Drop();
            foodItems[activeFoodSlot].isActiveSlot = true;
        }
    }
    public void RemoveActiveItem(ActiveItem newActiveItem) // Удаление активки в инвентарь
    {
        if(activeItems[activeAciveItemSlot].isEmpty)
        {
            activeItems[activeAciveItemSlot].Add(newActiveItem);
            activeItems[activeAciveItemSlot].isActiveSlot = true;
        }
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            activeFoodSlot++;   
            if(activeFoodSlot > foodItems.Count-1)
                activeFoodSlot = 0;
            ChangeSlot("FoodSlots",activeFoodSlot);
        }
        if(Input.GetKeyDown(KeyCode.LeftAlt)) 
        {
            activeAciveItemSlot++;   
            if(activeAciveItemSlot > activeItems.Count-1)
                activeAciveItemSlot = 0;
            ChangeSlot("ActiveItems",activeAciveItemSlot);
        }

    }

    private void ChangeSlot(string slotsName, int slotIndex) // FoodSlots - еда, ActiveItems - актвики
    {
        if(slotsName == "FoodSlots")
        {
            for(int i = 0; i < foodItems.Count; i++)
            {
                if(i != activeFoodSlot)
                {
                    if(foodItems[i].gameObject.active)
                        foodItems[i].gameObject.SetActive(false);
                }
                else
                    foodItems[i].gameObject.SetActive(true);
            }
        }
        else if(slotsName == "ActiveItems")
        {
            for(int i = 0; i < activeItems.Count; i++)
            {
                if(i != activeAciveItemSlot)
                {
                    if(activeItems[i].gameObject.active)
                        activeItems[i].gameObject.SetActive(false);
                }
                else
                    activeItems[i].gameObject.SetActive(true);
            }
        }
    }
}