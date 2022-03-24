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
        if(Input.GetKeyDown(KeyCode.E) & isOnTrigger)
            OpenBox();
    }

    private void OpenBox()
    {
        GetAllItemInChance();
        anim.SetBool("isOpen", true);
    }

    private void GetAllItemInChance()
    {
        foreach(GameObject item in gameManager.items)
        {
            
        }
    }
}
