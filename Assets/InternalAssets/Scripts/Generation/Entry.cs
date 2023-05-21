using UnityEngine;

namespace TheRat.LocationGeneration
{
    public sealed class Entry : MonoBehaviour
    {
        public Passage Passage { get; private set; }
        public RoomFactory RoomFactory { get; private set; }

        private void Awake()
        {
            Passage = GetComponent<Passage>();
            RoomFactory = GetComponent<RoomFactory>();
        }

        public void OpenPassage() 
            => Passage.Open();
        public void ClosePassage()
            => Passage.Close();

        public Room SpawnRoom()
            => RoomFactory.Create(Passage);
        public void DestroyRoom()
            => RoomFactory.Destroy();
    }
}