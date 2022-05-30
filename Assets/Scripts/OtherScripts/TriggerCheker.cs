using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoolMode
{
    Trigger,
    OtherScript
}

public class TriggerCheker : MonoBehaviour
{
    public BoolMode mode = BoolMode.Trigger;

    public bool trigger;

    public string targetTag = "Player";
    public Transform obj;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(mode == BoolMode.Trigger && other.tag == targetTag)
        {
            trigger = true;
            obj = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {   
        if(mode == BoolMode.Trigger && other.tag == targetTag)
        {
            trigger = false;
            obj = null;
        }
    }
}
