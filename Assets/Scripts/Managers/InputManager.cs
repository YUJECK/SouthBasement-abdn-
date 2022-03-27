using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode Sprint = KeyCode.LeftControl;

    //Управленеи инвентарем
    
    [Header("Inventory")]
    public KeyCode PickUpButton = KeyCode.E;
    public KeyCode FoodChangeButton = KeyCode.Tab;
    public KeyCode ActiveItemChangeButton = KeyCode.B;
    public KeyCode MelleWeaponChangeButton = KeyCode.LeftShift;
    public KeyCode FoodUseButton = KeyCode.Space;
}
