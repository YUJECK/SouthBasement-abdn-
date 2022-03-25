using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject item;
    private GameManager gameManager;
    private Animator anim;
    public bool isOnTrigger;
    public int chance;
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
    }

    private void GetAllItemInChance()
    {
        foreach(GameObject newItem in gameManager.items)
        {
            if(newItem.GetComponent<ItemInfo>().chanceOfDrop >= chance)
            {
                ItemsInThisChance.Add(newItem);
                gameManager.items.Remove(newItem);
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
            gameManager.SpawnCheese(spawnPoint,Random.Range(5,10));
    }
}
