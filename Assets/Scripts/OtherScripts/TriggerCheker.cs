using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheker : MonoBehaviour
{
    public bool isOnTrigger;
    public string targetTag = "Player";
    public Transform obj;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == targetTag)
        {
            isOnTrigger = true;
            obj = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {   
        if(other.tag == targetTag)
        {
            isOnTrigger = false;
            obj = null;
        }
    }
}
