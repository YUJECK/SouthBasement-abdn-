using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<FoodSlots> foodItems; // Слоты для еды
    public int ActiveFoodSlot; // Номер активного слота для игры
    public List<ActiveItemsSlots> activeItems; // Слоты для активок
    public int ActiveAciveItemSlot; // Номер активного слота для игры
    public List<PassiveItemsSlots> passiveItems; // Слоты для пассивок
    public int ActivePassiveSlot; // Номер активного слота для игры

    public void AddFood(FoodItem newFood) // Добавление еды в инвентарь
    {
        if(foodItems[ActiveFoodSlot].isEmpty)
        {
            foodItems[ActiveFoodSlot].Add(newFood);
            foodItems[ActiveFoodSlot].isActiveSlot = true;
        }
    }
    public void AddActiveItem(FoodItem newActiveItem) // Добавление активки в инвентарь
    {
        if(foodItems[ActiveAciveItemSlot].isEmpty)
        {
            foodItems[ActiveAciveItemSlot].Add(newActiveItem);
            foodItems[ActiveAciveItemSlot].isActiveSlot = true;
        }
    }
    public void AddPassiveItem(FoodItem newPassiveItem) // Добавление пассивки в инвентарь
    {
        if(foodItems[ActivePassiveSlot].isEmpty)
        {
            foodItems[ActivePassiveSlot].Add(newPassiveItem);
            foodItems[ActivePassiveSlot].isActiveSlot = true;
        }
    }

    public void RemoveFood(bool isDrop, int slotIndex) // Добавление еды в инвентарь
    {
        if(!foodItems[slotIndex].isEmpty)
        {
            foodItems[ActiveFoodSlot].Drop();
            foodItems[ActiveFoodSlot].isActiveSlot = true;
        }
    }
    public void RemoveActiveItem(FoodItem newActiveItem) // Добавление активки в инвентарь
    {
        if(foodItems[ActiveAciveItemSlot].isEmpty)
        {
            foodItems[ActiveAciveItemSlot].Add(newActiveItem);
            foodItems[ActiveAciveItemSlot].isActiveSlot = true;
        }
    }
    public void RemovePassiveItem(FoodItem newPassiveItem) // Добавление пассивки в инвентарь
    {
        if(foodItems[ActivePassiveSlot].isEmpty)
        {
            foodItems[ActivePassiveSlot].Add(newPassiveItem);
            foodItems[ActivePassiveSlot].isActiveSlot = true;
        }
    }
    
}
