using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItemsSlots : MonoBehaviour
{
    public FoodItem food;   // Предмет который лежит в этом слоте
    public GameObject objectOfItem;    // гейм обжект этого предмета

    public bool isEmpty; // Используется ли этот слот сейчас
    public bool isActiveSlot; // Используется ли этот слот сейчас

    public void Add(PassiveItem newPassiveItem) // Добавеление предмета
    {

    }
}
