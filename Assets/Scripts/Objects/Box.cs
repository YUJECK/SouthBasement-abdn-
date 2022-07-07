using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public List<ItemClass> itemsClassesToTrade;
    [Header("")]
    [SerializeField] private GameObject E;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject item;
    private GameManager gameManager;
    private Animator anim;
    [SerializeField] private bool isOnTrigger;
    private int chance;
    public bool isEmpty = false;
    public List<GameObject> ItemsInChance;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
       
        chance = Random.Range(0, 101);
        Invoke("GetAllItemInChance", 3f);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
            isOnTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
            isOnTrigger = false;
    }

    private void Update()
    {
        if (!isEmpty)
        {
            if (Input.GetKeyDown(KeyCode.E) & isOnTrigger)
                OpenBox();
        }
    }

    private void OpenBox()
    {
        anim.SetBool("isOpen", true);
        GetComponent<EButton>().Disable();
    }

    private void GetAllItemInChance()
    {
        if (itemsClassesToTrade.Contains(ItemClass.Food))
        {
            for (int i = 0; i < gameManager.Food.Count; i++)
                if(gameManager.Food[i].GetComponent<ItemInfo>().chanceOfDrop >= chance - PlayerStats.luck) ItemsInChance.Add(gameManager.Food[i]);
        }
        if (itemsClassesToTrade.Contains(ItemClass.MelleRangeWeapon))
        {
            for (int i = 0; i < gameManager.MelleRange.Count; i++)
                if (gameManager.MelleRange[i].GetComponent<ItemInfo>().chanceOfDrop >= chance - PlayerStats.luck) ItemsInChance.Add(gameManager.MelleRange[i]);
        }
        if (itemsClassesToTrade.Contains(ItemClass.ActiveItem))
        {
            for (int i = 0; i < gameManager.ActiveItems.Count; i++)
                if (gameManager.ActiveItems[i].GetComponent<ItemInfo>().chanceOfDrop >= chance - PlayerStats.luck) ItemsInChance.Add(gameManager.ActiveItems[i]);
        }
        if (itemsClassesToTrade.Contains(ItemClass.PassiveItem))
        {
            for (int i = 0; i < gameManager.PassiveItems.Count; i++)
                if (gameManager.PassiveItems[i].GetComponent<ItemInfo>().chanceOfDrop >= chance - PlayerStats.luck) ItemsInChance.Add(gameManager.PassiveItems[i]);
        }
    }

    public void SpawnItem()
    {
        if (ItemsInChance.Count != 0)
        {
            item = Instantiate(ItemsInChance[Random.Range(0, ItemsInChance.Count)], spawnPoint.position, Quaternion.identity);
            item.SetActive(true);
            isEmpty = true;
        }
        else
            gameManager.SpawnCheese(spawnPoint.position, Random.Range(8, 15));
    }
}
