using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement
{
    public sealed class FocusFireController : MonoBehaviour
    {
        [SerializeField] private FocusedFireMovement firePrefab;
        private List<SpawnPoint<FocusedFireMovement>> _spawnPoints = new();

        private void Awake()
        {
            List<Transform> points = new(GetComponentsInChildren<Transform>());

            points.Remove(transform);
            
            foreach (var point in points)
            {
                _spawnPoints.Add(new SpawnPoint<FocusedFireMovement>(point));
            }
        }

        public FocusedFireMovement Create()
        {
            foreach (var point in _spawnPoints)
            {
                if (!point.Spawned)
                    return Spawn(point);
            }

            return null;
        }

        private FocusedFireMovement Spawn(SpawnPoint<FocusedFireMovement> spawnPoint)
        {
            var instance = Instantiate(firePrefab, spawnPoint.Transform);
            spawnPoint.SetToThis(instance);

            return instance;
        }
    }
}