using Assets.InternalAssets.Scripts.Extensions;
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

        [SerializeField] private RoomFactory _startFactory;

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

        public void StartGeneration()
        {
            Queue<Room> roomsQueue = new();
            roomsQueue.Enqueue(SpawnStartRoom());

            while(_roomsSpawned.Count <= _roomsCount)
            {
                Room[] spawnedRooms = roomsQueue.Dequeue().UseFactories();

                roomsQueue.EnqueueRange(spawnedRooms);
                _roomsSpawned.AddRange(spawnedRooms);           
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
            foreach (RoomFactory roomFactory in _roomsSpawned[0].Factories)
                roomFactory.Destroy();
        }
    }
}