using System.Collections.Generic;
using TheRat.Extensions.DataStructures;
using TheRat.Helpers;
using TheRat.LocationGeneration;
using UnityEngine;

[RequireComponent(typeof(RoomPassagesContainer))]
public class PassagesRandomizer : MonoBehaviour
{
    private Dictionary<Directions, Passage> _passages;

    public Passage[] Passages => _passages.ToValueArray();

    public void Randomize()
    {
        if (_passages.Count < 2)
            return;

        int overallPassagesCount = UnityEngine.Random.Range(1, _passages.Count);

        while (_passages.Count > overallPassagesCount)
        {
            Directions passageToDelete = GetRandomDirection();

            if (_passages[passageToDelete].ExitVertex != null)
            {
                overallPassagesCount++;
                continue;
            }

            _passages[passageToDelete].Close();
            _passages.Remove(passageToDelete);
        }
    }

    private void Awake()
        => GetComponent<RoomPassagesContainer>().OnPassagesLoaded += AddPassagesToDictionary;

    private void AddPassagesToDictionary(Passage[] loadedPassages)
    {
        _passages = new Dictionary<Directions, Passage>();

        foreach (Passage passage in loadedPassages)
            _passages.Add(passage.Config.Direction, passage);
    }

    private Directions GetRandomDirection()
    {
        Directions[] allDirections = EnumHelper.GetAllDirections<Directions>();

        return allDirections[UnityEngine.Random.Range(0, allDirections.Length)];
    }
}