using UnityEngine;
using UnityEngine.UI;

public class FoodSlots : MonoBehaviour
{
    public FoodItem food;   // Предмет который лежит в этом слоте
    public GameObject objectOfItem;    // Гейм обжект этого предмета
    public Image slotIcon;   // Иконка предмета который лежит в этом слоте
    public bool isEmpty; // Используется ли этот слот сейчас
    public bool isActiveSlot; // Используется ли этот слот сейчас

    private void Update()
    {
        if(isActiveSlot & !isEmpty & Input.GetKeyDown(KeyCode.Space))
        {
            if(food.GetUses() <= 0)
            {
                Debug.Log("Uses : " + food.GetUses());
                Destroy(objectOfItem);
                Remove();
            }    
                
            food.itemAction.Invoke();
            Debug.Log("useItem");
        }
    }

    public void Add(FoodItem newFood, GameObject _objectOfItem) // Добавеление предмета
    {
        if(food != null)
            Drop();
            
        food = newFood;
        isEmpty = false;
        objectOfItem = _objectOfItem;
        slotIcon.sprite = newFood.sprite;
    }
    public void Drop() // Выброс предмета в игре
    {
        objectOfItem.SetActive(true);
        objectOfItem.transform.position = FindObjectOfType<Player>().GetComponent<Transform>().position;
        Remove();
    }
    public void Remove() // Удаление предмета из слота
    {
        food = null;
        objectOfItem = null;
        isEmpty = true;
        slotIcon.sprite = GameManager.hollowSprite;
    }
}
