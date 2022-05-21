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
        Start,
        OnLevelWasLoaded
    }
    [SerializeField] private float time;
    [SerializeField] private EventMode eventMode;
    [SerializeField] private StartMode startMode;
    [SerializeField] private UnityEvent Event = new UnityEvent();
    [SerializeField] private GameObject Object;

    private void Start()
    {
        if(startMode == StartMode.Start) StartCoroutine(Disable());
    } 
    private void OnLevelWasLoaded(int level)
    {
        if(startMode == StartMode.OnLevelWasLoaded) StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        Object.SetActive(true);
        
        yield return new WaitForSeconds(time);
        
        if(eventMode == EventMode.Destroy){
            Destroy(Object);
            Event.Invoke();
        } 
        else{
            Object.SetActive(false);
            Event.Invoke();
        }
    }
}
