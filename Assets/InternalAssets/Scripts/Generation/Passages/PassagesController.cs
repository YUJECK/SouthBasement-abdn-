using System.Collections.Generic;
using TheRat.LocationGeneration;
using UnityEngine;

public class PassagesController : MonoBehaviour
{
    private Dictionary<Directions, Passage> passages = new();

    private void Awake()
        => AddPassages();

    public Passage Get(Directions direction)
        => passages[direction];

    public void Close(Directions direction)
        => passages[direction].Close();
    public void Open(Directions direction)
        => passages[direction].Open();

    private void AddPassages()
    {
        Passage[] allPassages = GetComponentsInChildren<Passage>();

        foreach (Passage passage in allPassages)
        {
            if (!passages.TryAdd(passage.Config.Direction, passage))
                Debug.LogWarning("Extra passage was found", gameObject);
        }

        if(passages.Count < 4)
            Debug.LogWarning($"{4 - passages.Count} passages were lost", gameObject);
    }
}