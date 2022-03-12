using UnityEngine;
using UnityEngine.UI;

public class FoodSlots : MonoBehaviour
{
    public FoodItem food;   // Предмет который лежит в этом слоте
    public GameObject objectOfItem;    // Гейм обжект этого предмета
    [SerializeField] private Image slotIcon;   // Иконка предмета который лежит в этом слоте
    public bool isEmpty; // Используется ли этот слот сейчас
    public bool isActiveSlot; // Используется ли этот слот сейчас

    public void Add(FoodItem newFood) // Добавеление предмета
    {
        if(food != null)
            Drop();
            
        food = newFood;
        slotIcon.sprite = newFood.sprite;
    }

    public void Drop() // Выброс предмета в игре
    {
        objectOfItem.SetActive(true);
        objectOfItem.transform.position = FindObjectOfType<Player>().GetComponent<Transform>().position;
        food = null;
    }
 
    public void Remove() // Удаление предмета из слота
    {
        food = null;
        slotIcon.sprite = GameManager.hollowSprite;
    }
}
