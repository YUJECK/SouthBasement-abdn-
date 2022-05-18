using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject E;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject item;
    private GameManager gameManager;
    private Animator anim;
    [SerializeField] private bool isOnTrigger;
    private int chance;
    public bool isEmpty = false;
    public List<GameObject> ItemsInThisChance;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        chance = Random.Range(0,101);
        Invoke("GetAllItemInChance",3f);
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
        if(!isEmpty)
        {
            if(Input.GetKeyDown(KeyCode.E) & isOnTrigger)
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
        for(int i = 0; i < gameManager.items.Count; i++)
        {
            if(gameManager.items[i].GetComponent<ItemInfo>().chanceOfDrop >= chance-10)
            {
                ItemsInThisChance.Add(gameManager.items[i]);
                gameManager.items.Remove(gameManager.items[i]);
            }
        }
    }

    public void SpawnItem()
    {
        if(ItemsInThisChance.Count != 0)
        {
            item = Instantiate(ItemsInThisChance[Random.Range(0,ItemsInThisChance.Count)],spawnPoint.position,Quaternion.identity);
            item.SetActive(true);
            isEmpty = true;
        }
        else
            gameManager.SpawnCheese(spawnPoint.position, Random.Range(5,10));
    }
}
