using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    //variables
    private bool isOnTrigger;
    [SerializeField] private List<string> enterTags = new List<string>();
    private List<GameObject> objects = new List<GameObject>();

    //getters
    public bool IsOnTrigger => isOnTrigger;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (enterTags.Contains(collision.tag))
        {
            objects.Add(collision.gameObject);
            isOnTrigger = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (objects.Contains(collision.gameObject))
            objects.Remove(collision.gameObject);
        if(objects.Count == 0)
            isOnTrigger = false; 
    }
}
