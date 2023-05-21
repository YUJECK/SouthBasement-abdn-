using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheRat.LocationGeneration
{
    [RequireComponent(typeof(RoomEntrysController))]
    public sealed class RoomEntriesContainer : MonoBehaviour
    {
        private Dictionary<Directions, Entry> _entries = new();

        public event Action<Entry[]> OnPassagesLoaded;

        private void Start()
        {
            Entry[] entries = GetComponentsInChildren<Entry>();

            if (entries.Length == 0)
            {
                Debug.LogWarning("No passages in children");
                return;
            }

            else if (entries.Length < 4)
                Debug.LogWarning("Fewer passes than necessary were found");

            else if (entries.Length > 4)
                Debug.LogWarning("Extra passages were found");

            WriteEntriesDictionary(entries);
            OnPassagesLoaded?.Invoke(entries);
        }

        public Entry Get(Directions direction)
            => _entries[direction];

        private void WriteEntriesDictionary(Entry[] allPassages)
        {
            foreach (Entry entry in allPassages)
            {
                if (!_entries.TryAdd(entry.Passage.Config.Direction, entry))
                    Debug.LogWarning("Extra passage was found", gameObject);
            }
        }
    }
}