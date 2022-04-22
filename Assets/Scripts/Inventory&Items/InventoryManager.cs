using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<FoodSlots> foodItems; // Слоты для еды
    public int activeFoodSlot; // Номер активного слота для игры

    [Header("")]
    public List<ActiveItemsSlots> activeItems; // Слоты для активок
    public int activeAciveItemSlot; // Номер активного слота для игры
    public Slider activeItemChargeSlider; // Визуализация зарядки активки

    [Header("")]
    public List<MelleWeaponSlot> melleWeapons; // Слоты для оружия ближнего боя
    public int melleRangeActiveSlot; // Номер активного слота для игры
    
    [Header("")]
    public List<PassiveItemsSlots> passiveItems; // Слоты для пассивок

    private RatAttack ratAttack;
    private InputManager inputManager;

    private void Awake()
    {
        ratAttack = FindObjectOfType<RatAttack>();
        inputManager = FindObjectOfType<InputManager>();
    }

    public void AddFood(FoodItem newFood, GameObject objectOfItem) // Добавление еды в инвентарь
    {
        foodItems[activeFoodSlot].Add(newFood,objectOfItem);
    }
    public void AddActiveItem(ActiveItem newActiveItem, GameObject objectOfItem) // Добавление активки в инвентарь
    {
        activeItems[activeAciveItemSlot].Add(newActiveItem, objectOfItem);
    }
    public void AddMelleWeapon(MelleRangeWeapon newMelleWeapon, GameObject objectOfItem) // Добавление активки в инвентарь
    {
        melleWeapons[melleRangeActiveSlot].Add(newMelleWeapon, objectOfItem);
        ratAttack.SetMelleWeapon(melleWeapons[melleRangeActiveSlot].melleWeapon); // Ставим оружие в активное
    }

    //Удаление из инвенторя

    public void RemoveFood(bool isDrop, int slotIndex) // Удаление еды в инвентарь
    {
        if(!foodItems[slotIndex].isEmpty)
            foodItems[activeFoodSlot].Drop();
    }
    public void RemoveActiveItem(bool isDrop, int slotIndex) // Удаление активки в инвентарь
    {
        if(!activeItems[slotIndex].isEmpty)
            activeItems[activeAciveItemSlot].Drop();
    }
    public void RemoveMelleWeapon(bool isDrop, int slotIndex) // Удаление активки в инвентарь
    {
        if(!melleWeapons[slotIndex].isEmpty)
            melleWeapons[melleRangeActiveSlot].Drop();
    }
   
    void Update()
    {
        if(Input.GetKeyDown(inputManager.FoodChangeButton)) // Смена слота с едой
        {
            activeFoodSlot++;   
            if(activeFoodSlot > foodItems.Count-1)
                activeFoodSlot = 0;
            ChangeSlot("FoodSlots",activeFoodSlot);
        }
        if(Input.GetKeyDown(inputManager.ActiveItemChangeButton)) // Смена слота с активкой
        {
            activeAciveItemSlot++;   

            if(activeAciveItemSlot > activeItems.Count-1)
                activeAciveItemSlot = 0;
            ChangeSlot("ActiveItems",activeAciveItemSlot);
        }

        //Смена слотов миллишного оружия
        if(Input.GetKeyDown(inputManager.MelleWeaponChangeButton)) // Смена оружия ближнего боя
        {
            melleRangeActiveSlot++;   

            if(melleRangeActiveSlot > melleWeapons.Count-1)
                melleRangeActiveSlot = 0;
            ChangeSlot("MelleRange",melleRangeActiveSlot);
        }
        // if(inputManager.mouseWheel < -0.1f) // Откручивание назад
        // {
        //     melleRangeActiveSlot--;   

        //     if(melleRangeActiveSlot < 0)
        //         melleRangeActiveSlot = 0;
        //     ChangeSlot("MelleRange",melleRangeActiveSlot);
        // }
    }

    private void ChangeSlot(string slotsName, int slotIndex) // MelleRange - ближний бой, FoodSlots - еда, ActiveItems - актвики
    {
        if(slotsName == "FoodSlots") // Смена слота с едой 
        {
            for(int i = 0; i < foodItems.Count; i++)// Ищем нужный слот и оключаем не активные
            {
                if(i != activeFoodSlot)
                {
                    if(foodItems[i].gameObject.active)
                    {
                        foodItems[i].gameObject.SetActive(false);
                         foodItems[i].isActiveSlot = false;
                    }
                }
                else
                {
                    foodItems[i].gameObject.SetActive(true);
                    foodItems[i].isActiveSlot = true;
                }
            }
        }
        else if(slotsName == "ActiveItems") // Смена слота с активкой
        {
            for(int i = 0; i < activeItems.Count; i++)// Ищем нужный слот и оключаем не активные
            {
                if(i != activeAciveItemSlot)
                {
                    if(activeItems[i].gameObject.active)
                    {
                        activeItems[i].gameObject.SetActive(false);
                        activeItems[i].isActiveSlot = false;
                    }
                }
                else
                {
                    activeItems[i].gameObject.SetActive(true);
                    activeItems[i].isActiveSlot = true;
                }
            }
        }
        else if(slotsName == "MelleRange") // Смена слота с оружием ближнего боя
        {
            for(int i = 0; i < melleWeapons.Count; i++) // Ищем нужный слот и оключаем не активные
            {
                if(i != melleRangeActiveSlot)
                {
                    if(melleWeapons[i].gameObject.active)
                    {
                        melleWeapons[i].gameObject.SetActive(false);
                        melleWeapons[i].isActiveSlot = false;
                    }
                }
                else
                {
                    melleWeapons[i].gameObject.SetActive(true);
                    melleWeapons[i].isActiveSlot = true;
                }
            }

            if(!melleWeapons[melleRangeActiveSlot].isEmpty) // Ставим оружие в активное для игрока
                ratAttack.SetMelleWeapon(melleWeapons[melleRangeActiveSlot].melleWeapon);
        }
    }
}   