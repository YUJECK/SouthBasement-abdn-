using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Рахные спрайты к которым нужен быстрый доступ
    public Sprite hollowSprite;
    
    [Header("Индикаторы еффектов")]
    public Sprite BurnIndicator;
    public Sprite PoisonedIndicator;
    public Sprite BleedIndicator;

    [Header("")]
    public int LevelCounter = 1; // Счетчик уровней
    public List<GameObject> items; // Лист предметов
    public List<GameObject> traderItems; // Лист предметов которые продаются

    [Header("Сыр и все что с ним связано")]

    public int playerCheese; // Счетчик сыра игрока
    public GameObject CheesePrefab; //Префаб сыра
    public Text CheeseText; // Счетчик сыра(В UI)

    [Header("Диалоги")]
    public static bool isActiveAnyPanel = false; // Есть ли активная панель выбора(При ней игрок ничено не может делать)

    public static GameManager instance; // Синглтоп

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnLevelWasLoaded()
    {
        if(traderItems.Count != 0) //Записываем все не проданные предметы обратно
        {
            int itemsCount = traderItems.Count;
              
            for(int i = 0; i < itemsCount; i++)
            {
                items.Add(traderItems[0]);
                traderItems.Remove(traderItems[0]);
            }  
        }    
    }


    public void SpawnCheese(Vector3 CheesePos, int cheeseCount) // Справнит сыр
    {
        Debug.Log("CheeseCount " + cheeseCount);
        GameObject cheese = Instantiate(CheesePrefab, CheesePos, Quaternion.identity);
        cheese.GetComponent<Cheese>().cheeseScore = cheeseCount;
    }
    public void CheeseScore(int NewCheese) // Зачисляет сыр
    {
        playerCheese += NewCheese;
        CheeseText.text = playerCheese.ToString();
    }
}
