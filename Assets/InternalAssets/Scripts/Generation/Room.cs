using System.Collections.Generic;
using UnityEngine;

namespace TheRat.LocationGeneration
{
    public class Room : MonoBehaviour
    {
        [field: SerializeField] public int Chance { get; private set; }
        [field: SerializeField] public List<RoomFactory> Factories { get; private set; }

        public Room[] UseFactories()
        {
            RandomizeFactories();
        
            List<Room> spawnedRooms = new List<Room>();

            foreach(RoomFactory roomFactory in Factories)
                spawnedRooms.Add(roomFactory.Create());

            return spawnedRooms.ToArray();
        }

        private void RandomizeFactories()
        {
            int factoriesCount = Random.Range(2, Factories.Count);

            for (int i = 0; i < Factories.Count - factoriesCount; i++)
                Factories.RemoveAt(Random.Range(0, Factories.Count));
        }
    }
}