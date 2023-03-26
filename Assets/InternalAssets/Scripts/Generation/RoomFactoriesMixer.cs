using System;
using System.Collections.Generic;

namespace TheRat.LocationGeneration
{
    public sealed class RoomFactoriesMixer
    {
        private Dictionary<Directions, IRoomFactory> Factories = new();

        public RoomFactoriesMixer(IRoomFactory[] factories, bool randomize)
        {
            foreach (var factory in factories)
                Factories.TryAdd(factory.Direction, factory);

            if (randomize)
                RandomizeFactories();
        }

        public Room[] CreateAll()
            => SpawnRooms().ToArray();

        public void DestroyAll()
        {
            foreach (var item in Factories)
                item.Value?.Destroy();
        }

        private List<Room> SpawnRooms()
        {
            List<Room> spawnedRooms = new();

            foreach (var roomFactory in Factories)
            { 
                if(roomFactory.Value != null)
                    spawnedRooms.Add(roomFactory.Value.Create());
            }

            return spawnedRooms;
        }
        private void RandomizeFactories(params Directions[] without)
        {
            List<Directions> directions = new(GetAllDirections());

            foreach (var direction in without)
                directions.Remove(direction);

            int roomsRemove = UnityEngine.Random.Range(0, directions.Count);

            for (int i = 0; i < roomsRemove; i++)
                directions.RemoveAt(UnityEngine.Random.Range(0, roomsRemove - i));
        }

        private static Directions[] GetAllDirections()
            => (Directions[])Enum.GetValues(typeof(Directions));
    }
}