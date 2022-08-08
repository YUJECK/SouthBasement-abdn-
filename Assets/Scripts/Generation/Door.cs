using EnemysAI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject doors;
    private bool isClosed = false;
    private int enemysCount = 0;
    public UnityEvent onClose = new UnityEvent();
    public UnityEvent onOpen = new UnityEvent();
    public List<GameObject> targetSelections = new List<GameObject>();

    public void IncreaseEnemysCount() => enemysCount++;
    public void ReduceEnemysCount()
    {
        enemysCount--;
        if (enemysCount <= 0)
        {
            OpenDoors();
            Destroy(gameObject);
        }
    }
    public bool IsClosed => isClosed;
    public void CloseDoors()
    {
        if(enemysCount > 0)
        {
            doors.SetActive(true); isClosed = true;
            for (int i = 0; i < enemysCount; i++)
                targetSelections[targetSelections.Count - 1].SetActive(true);
        }
    }
    public void OpenDoors() { doors.SetActive(false); isClosed = false; }

    private void Start()
    {
        enemysCount = transform.childCount;
        for (int i = 0; i < enemysCount; i++)
        {
            transform.GetChild(i).GetComponent<EnemyHealth>().onDie.AddListener(ReduceEnemysCount);
            targetSelections.Add(transform.GetChild(i).GetComponentInChildren<TargetSelection>().gameObject);
            Utility.InvokeMethod<bool>(targetSelections[targetSelections.Count - 1].SetActive, false, 0.1f);
        }
    }
}