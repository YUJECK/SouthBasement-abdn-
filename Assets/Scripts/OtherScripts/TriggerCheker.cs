using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCheker : MonoBehaviour
{
    [SerializeField] private ChangeMode changeMode = ChangeMode.Trigger;
    [SerializeField] private TriggerMode triggerMode = TriggerMode.OnlyAsTag;

    public UnityEvent<GameObject> onEnter = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onExit = new UnityEvent<GameObject>();
    public GameObject nowEnter;
    public GameObject nowExit;
    public bool trigger;
    public string targetTag = "Player";
    public Transform obj = null;
    public List<Transform> other = new List<Transform>();

    private void Update()
    {
        if (changeMode == ChangeMode.Trigger && trigger && obj == null) trigger = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        nowEnter = other.gameObject;
        onEnter.Invoke(nowEnter);
        trigger = true;

        if (changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.OnlyAsTag && other.CompareTag(targetTag))
            obj = other.transform;
        else if (changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.All)
            this.other.Add(nowEnter.transform); 
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        nowExit = other.gameObject;
        onExit.Invoke(nowExit);
        trigger = false;

        if(changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.OnlyAsTag && other.CompareTag(targetTag))
            obj = null;
        else if (changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.All)
            this.other.Remove(nowExit.transform);
    }
    
    public enum TriggerMode
    {
        OnlyAsTag,
        All
    }
    public enum ChangeMode
    {
        Trigger,
        OtherScript
    }
}
