using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerChecker : MonoBehaviour
{
    [Header("Настройки")]
    public List<string> targetTags = new List<string>();
    public bool onTriggerEnter = true;
    public bool onTriggerStay = false;
    public bool onTriggerExit = true;

    [Header("События")]
    public UnityEvent<GameObject> onEnter = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onStay = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onExit = new UnityEvent<GameObject>();
    
    //Другое
    private Transform _triggerObject = null;
    public Transform triggerObject
    {
        get { return _triggerObject; }
        private set => _triggerObject = value;
    }
    [HideInInspector] public bool trigger;

    private void Start()
    {
        if(targetTags.Count == 0) targetTags.Add("Player");
    }

    private void OnTriggerEnter2D(Collider2D enterCollider)
    {   
        if (onTriggerEnter && targetTags.Contains(enterCollider.tag))
        {
            trigger = true;
            _triggerObject = enterCollider.transform;
            onEnter.Invoke(enterCollider.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D stayCollider)
    {   
        if (onTriggerStay && targetTags.Contains(stayCollider.tag))
        {
            trigger = true;
            onStay.Invoke(stayCollider.gameObject);
            _triggerObject = stayCollider.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D exitCollider)
    {
        if (onTriggerExit && targetTags.Contains(exitCollider.tag))
        {
            trigger = false;
            onExit.Invoke(exitCollider.gameObject);
            _triggerObject = null;
        }
    }    
}
