using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int LevelCounter = 1;
    public List<GameObject> items;

    [Header("Сыр и все что с ним связано")]

    public int playerCheese;
    public GameObject CheesePrefab;
    public Text CheeseText;

    [Header("Диалоги")]
    public static bool isActiveAnyPanel = false;

    public static GameManager instance;

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
    public void SpawnCheese(GameObject Enemy)
    {
        Instantiate(CheesePrefab, Enemy.transform.position, CheesePrefab.transform.rotation);
    }
    public void CheeseScore(int NewCheese)
    {
        playerCheese += NewCheese;
        CheeseText.text = playerCheese.ToString();
    }
}
