using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace TheRat.LocationGeneration
{
    public sealed class FactoryMixer
    {
        private readonly List<RoomFactory> _roomFactories;

        public FactoryMixer(RoomFactory[] roomFactories)
            => _roomFactories = new(roomFactories);

        public void Add(RoomFactory roomFactory)
        {
            if (roomFactory == null)
            {
                Debug.Log("You tried to add null factory");
                return;
            }

            _roomFactories.Add(roomFactory);
        }

        public async void CreateAll()
        {
            foreach (RoomFactory roomFactory in _roomFactories)
            {
                //roomFactory.Create();

                await UniTask.Delay(100);
            }
        }
    }
}