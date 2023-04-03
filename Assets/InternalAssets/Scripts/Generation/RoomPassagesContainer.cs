using System;
using UnityEngine;

namespace TheRat.LocationGeneration
{
    public sealed class RoomPassagesContainer : MonoBehaviour
    {
        public event Action<Passage[]> OnPassagesLoaded;

        private void Start()
        {
            Passage[] passages = GetComponentsInChildren<Passage>();

            if (passages.Length == 0)
            {
                Debug.LogWarning("No passages in children");
                return;
            }

            else if (passages.Length < 4)
                Debug.LogWarning("Fewer passes than necessary were found");

            else if (passages.Length > 4)
                Debug.LogWarning("Extra passages were found");

            OnPassagesLoaded?.Invoke(passages);
        }
    }
}