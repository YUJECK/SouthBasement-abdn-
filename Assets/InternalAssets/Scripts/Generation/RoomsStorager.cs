using System.Collections.Generic;
using TheRat.Helpers;
using UnityEngine;

namespace TheRat.LocationGeneration
{
    [CreateAssetMenu()]
    public sealed class RoomsStorager : ScriptableObject
    {
        [field: SerializeField] public List<Room> EnemyRooms { get; private set; }
        [field: SerializeField] public List<Room> StartRooms { get; private set; }

        public Room GetRandomRoom(List<Room> rooms)
        {
            Room randomRoom = RandomizeRoom();

            while (randomRoom.SpawnChance >= ChanceSystem.GetChance())
                return randomRoom;

            return null;

            Room RandomizeRoom() => rooms[UnityEngine.Random.Range(0, rooms.Count)];
        }
    }
}