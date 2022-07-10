using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerChecker : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private ChangeMode changeMode = ChangeMode.Trigger;
    [SerializeField] private TriggerMode triggerMode = TriggerMode.OnlyAsTag;
    public string targetTag = "Player";

    [Header("События")]
    public UnityEvent<GameObject> onEnter = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onStay = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onExit = new UnityEvent<GameObject>();
    [Header("Объекты")]
    public GameObject nowEnter;
    public GameObject nowStay;
    public GameObject nowExit;
    public Transform triggerObject = null;
    public List<Transform> otherObjects = new List<Transform>();
    [HideInInspector] public bool trigger;

    private void Update()
    {
        if (changeMode == ChangeMode.Trigger && trigger && triggerObject == null) trigger = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.OnlyAsTag && other.CompareTag(targetTag))
        {
            trigger = true;
            nowEnter = other.gameObject;
            onEnter.Invoke(nowEnter);
            triggerObject = other.transform;
        }
        else if (changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.All)
        {
            trigger = true;
            nowEnter = other.gameObject;
            onEnter.Invoke(nowEnter);
            this.otherObjects.Add(nowEnter.transform);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {   
        if (changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.OnlyAsTag && other.CompareTag(targetTag))
        {
            trigger = true;
            onStay.Invoke(nowStay);
            nowStay = other.gameObject;
            triggerObject = other.transform;
        }
        else if (changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.All)
        {
            trigger = true;
            onStay.Invoke(nowStay);
            nowStay = other.gameObject;
            this.otherObjects.Add(nowEnter.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.OnlyAsTag && other.CompareTag(targetTag))
        {
            trigger = false;
            nowExit = other.gameObject;
            onExit.Invoke(nowExit);
            triggerObject = null;
        }
        else if (changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.All)
        {
            trigger = false;
            nowExit = other.gameObject;
            onExit.Invoke(nowExit);
            this.otherObjects.Remove(nowExit.transform);
        }
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
