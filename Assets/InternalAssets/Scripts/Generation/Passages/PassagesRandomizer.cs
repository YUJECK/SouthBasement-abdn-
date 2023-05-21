using System.Collections.Generic;
using TheRat.Extensions.DataStructures;
using TheRat.Helpers;
using TheRat.LocationGeneration;
using UnityEngine;

[RequireComponent(typeof(RoomEntriesContainer))]
public class PassagesRandomizer : MonoBehaviour
{
    private Dictionary<Directions, Entry> _passages;

    public Entry[] Passages => _passages.ToValueArray();

    public void Randomize()
    {
        if (_passages.Count < 2)
            return;

        int overallPassagesCount = UnityEngine.Random.Range(1, _passages.Count);

        while (_passages.Count > overallPassagesCount)
        {
            Directions passageToDelete = GetRandomDirection();

            if (_passages[passageToDelete].Passage.ExitVertex != null)
            {
                overallPassagesCount++;
                continue;
            }

            _passages[passageToDelete].Passage.Close();
            _passages.Remove(passageToDelete);
        }
    }

    private void Awake()
        => GetComponent<RoomEntriesContainer>().OnPassagesLoaded += AddPassagesToDictionary;

    private void AddPassagesToDictionary(Entry[] loadedPassages)
    {
        _passages = new Dictionary<Directions, Entry>();

        foreach (Entry entry in loadedPassages)
            _passages.Add(entry.Passage.Config.Direction, entry);
    }

    private Directions GetRandomDirection()
    {
        Directions[] allDirections = EnumHelper.GetAllDirections<Directions>();

        return allDirections[UnityEngine.Random.Range(0, allDirections.Length)];
    }
}