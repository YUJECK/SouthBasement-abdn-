using Assets.InternalAssets.Scripts.Extensions;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TheRat.Helpers;
using UnityEngine;
using Zenject;

namespace TheRat.LocationGeneration
{
    public class LocationGenerator : MonoBehaviour, IGeneratorController
    {
        [SerializeField] private int _roomsCount = 14;
        [SerializeField] private Transform _startPoint;

        [SerializeField] private EnemyRoomFactory _startFactory;

        public event Action OnGenerationStarted;
        public event Action OnGenerationEnded;

        private RoomsStorager _roomsStorager;
        private ContainersHelper _containersHelper;

        private List<Room> _roomsSpawned = new();

        [Inject]
        public void Construct(RoomsStorager roomsStorager, ContainersHelper containersHelper)
        {
            _roomsStorager = roomsStorager;
            _containersHelper = containersHelper;
        }

        private void Start()
        {
            StartGeneration();
        }

        public async void StartGeneration()
        {
            Queue<Room> roomsQueue = new();
            roomsQueue.Enqueue(SpawnStartRoom());

            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));

            while (_roomsSpawned.Count < _roomsCount)
            {
                Room[] spawnedRooms = roomsQueue
                    .Dequeue()
                    .RoomFactoryMixer
                    .CreateAll();

                

                roomsQueue.EnqueueRange(spawnedRooms);
                _roomsSpawned.AddRange(spawnedRooms);

                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            }
        }

        private Room SpawnStartRoom()
        {
            Room startRoom = _startFactory.Create();

            _roomsSpawned.Add(startRoom);

            return startRoom;
        }

        public void Regenerate()
        {
            _roomsSpawned[0].RoomFactoryMixer.DestroyAll();
        }
    }
}