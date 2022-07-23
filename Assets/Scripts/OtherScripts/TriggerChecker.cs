using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerChecker : MonoBehaviour
{
    [Header("Настройки")]
    public List<string> targetTags = new List<string>();

    [Header("События")]
    public UnityEvent<GameObject> onEnter = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onStay = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onExit = new UnityEvent<GameObject>();
    
    [Header("Объекты")]
    public Transform triggerObject = null;
    [HideInInspector] public bool trigger;

    private void Start()
    {
        if(targetTags.Count == 0) targetTags.Add("Player");
    }

    private void OnTriggerEnter2D(Collider2D enterCollider)
    {   
        if (targetTags.Contains(enterCollider.tag))
        {
            trigger = true;
            triggerObject = enterCollider.transform;
            onEnter.Invoke(enterCollider.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D stayCollider)
    {   
        if (targetTags.Contains(stayCollider.tag))
        {
            trigger = true;
            onStay.Invoke(stayCollider.gameObject);
            triggerObject = stayCollider.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D exitCollider)
    {
        if (targetTags.Contains(exitCollider.tag))
        {
            trigger = false;
            onExit.Invoke(exitCollider.gameObject);
            triggerObject = null;
        }
    }    
}
