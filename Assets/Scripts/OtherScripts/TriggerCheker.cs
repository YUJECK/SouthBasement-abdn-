using System.Collections.Generic;
using UnityEngine;

public class TriggerCheker : MonoBehaviour
{
    [SerializeField] private ChangeMode changeMode = ChangeMode.Trigger;
    [SerializeField] private TriggerMode triggerMode = TriggerMode.OnlyAsTag;

    public bool trigger;
    public string targetTag = "Player";
    public Transform obj;
    public List<Transform> other = new List<Transform>();
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.OnlyAsTag && other.CompareTag(targetTag))
        {
            trigger = true;
            obj = other.transform;
        }
        else if(changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.All)
            this.other.Add(other.transform);
    }
    private void OnTriggerExit2D(Collider2D other)
    {   
        if(changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.OnlyAsTag && other.CompareTag(targetTag))
        {
            trigger = false;
            obj = null;
        }
        else if (changeMode == ChangeMode.Trigger && triggerMode == TriggerMode.All)
        {
            this.other.Remove(other.transform);
            trigger = false;
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
