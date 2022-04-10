using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheker : MonoBehaviour
{
    public bool isOnTrigger;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
            isOnTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {   
        if(other.tag == "Player")
            isOnTrigger = false;
    }
}
