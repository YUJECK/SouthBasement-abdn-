using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickUp : MonoBehaviour
{
    public FoodItem food;
    private InventoryManager inventory;

    private bool isOnTrigger = false;
    private bool isWhiteSprite = false;


    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = food.sprite;
        inventory = FindObjectOfType<InventoryManager>();
    } 

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            isOnTrigger = true;
    }
    
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            isOnTrigger = false;
    }
    private void Update()
    {
        //Смена обычного спрайта на спрайт с обводкой и наоборот
        if(isOnTrigger & !isWhiteSprite)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = food.WhiteSprite;
            isWhiteSprite = true;
        }
        else if(!isOnTrigger & isWhiteSprite)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = food.sprite;
            isWhiteSprite = false;
        }

        if(isOnTrigger & Input.GetKeyDown(KeyCode.E))
        {
            inventory.AddFood(food, gameObject);
            gameObject.SetActive(false);
        }
    }
}
