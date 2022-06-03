using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TempObject : MonoBehaviour
{
    private enum EventMode
    {
        Destroy,
        Disable
    }
    private enum StartMode
    {
        Awake,
        Start,
        OnLevelWasLoaded
    }
    
    [SerializeField] private float time;
    [SerializeField] private EventMode eventMode;
    [SerializeField] private StartMode startMode;
    [SerializeField] private UnityEvent StartEvent = new UnityEvent();
    [SerializeField] private UnityEvent EndEvent = new UnityEvent();
    [SerializeField] private GameObject Object;

    private void Awake()
    {
        if (startMode == StartMode.Awake)
        {
            StartEvent.Invoke();
            StartCoroutine(Disable());
        }
    }
    private void Start()
    {
        if (startMode == StartMode.Start)
        {
            StartEvent.Invoke();
            StartCoroutine(Disable());
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        if (startMode == StartMode.OnLevelWasLoaded)
        {
            StartEvent.Invoke();
            StartCoroutine(Disable());
        }
    }

    private IEnumerator Disable()
    {
        Object.SetActive(true);
        
        yield return new WaitForSeconds(time);
        
        if(eventMode == EventMode.Destroy){
            EndEvent.Invoke();
            Destroy(Object);
        } 
        else{
            EndEvent.Invoke();
            Object.SetActive(false);
        }
    }
}
