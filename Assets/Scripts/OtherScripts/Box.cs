// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Box : MonoBehaviour
// {
//     public Transform spawnPoint;
//     public GameObject item;
//     GameManager GameManager;
//     Animator anim;
//     public bool isOnTrigger;
//     public int chance;
//     public bool isEmpty = false;
//     public List<GameObject> ItemsInThisChance;

//     void Start()
//     {
//         GameManager = FindObjectOfType<GameManager>();
//         anim = GetComponent<Animator>();
//     }
//     private void Update()
//     {
//         if(isOnTrigger)
//         {
//             if(Input.GetKeyDown(KeyCode.E))
//                 OpenBox();
//         }
//     }
//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.tag == "Player")
//             isOnTrigger = true;
//     }
//     private void OnTriggerExit2D(Collider2D collision)
//     {
//         if (collision.tag == "Player")
//             isOnTrigger = false;
//     }
//     void OpenBox()
//     {
//         anim.SetBool("isOpen", true);
//         GetComponent<EButton>().isActive = false;
//         if (!isEmpty) { isEmpty = true; }
//     }

//     void SpawnItem()
//     {
//         chance = Random.Range(0, 100);

//         ItemsInChance(chance);
//         if (ItemsInThisChance.Count != 0)
//         {
//             int itemIndex = Random.Range(0, ItemsInThisChance.Count);
//             item = ItemsInThisChance[itemIndex];
//             Instantiate(item, spawnPoint.position, spawnPoint.rotation, spawnPoint);
//             GameManager.items.Remove(GameManager.items[itemIndex]);
//         }
//         else
//             Instantiate(GameManager.CheesePrefab, spawnPoint.position, spawnPoint.rotation);
//     }

//     void ItemsInChance(int chance)
//     {
//         for (int i = 0; i < GameManager.items.Count; i++)
//         {
//             if (GameManager.items[i].GetComponent<ItemInGame>().item != null)
//             {
//                 if (GameManager.items[i].GetComponent<ItemInGame>().item.ChanceOfDrop >= chance)
//                 ItemsInThisChance.Add(GameManager.items[i]);
//             }
//             else if (GameManager.items[i].GetComponent<ItemInGame>().weapon != null)
//             {
//                 if (GameManager.items[i].GetComponent<ItemInGame>().weapon.ChanceOfDrop >= chance)
//                 ItemsInThisChance.Add(GameManager.items[i]);
//             }
//         }
//     }
// }
