using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode Sprint = KeyCode.LeftControl;

    //Управленеи инвентарем
    
    [Header("Inventory")]
    public float mouseWheel = 0f; //Считывание колесика мыши
    public KeyCode PickUpButton = KeyCode.E;
    public KeyCode FoodChangeButton = KeyCode.Tab;
    public KeyCode ActiveItemChangeButton = KeyCode.B;
    public KeyCode MelleWeaponChangeButton = KeyCode.LeftShift;
    public KeyCode FoodUseButton = KeyCode.Space;

    void Update() { mouseWheel = Input.GetAxis("Mouse ScrollWheel"); }
}
