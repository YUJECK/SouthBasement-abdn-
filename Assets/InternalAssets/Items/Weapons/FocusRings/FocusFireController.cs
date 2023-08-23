using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement
{
    public sealed class FocusFireController : MonoBehaviour
    {
        [SerializeField] private FocusedFire firePrefab;
        
        private readonly List<SpawnPoint<FocusedFire>> _spawnPoints = new();
        private FocusRings _ringsInstance;

        private void Awake()
        {
            List<Transform> points = new(GetComponentsInChildren<Transform>());

            points.Remove(transform);
            
            foreach (var point in points)
            {
                _spawnPoints.Add(new SpawnPoint<FocusedFire>(point));
            }
        }

        public void InitRingsInstance(FocusRings rings)
        {
            _ringsInstance = rings;
        }
        
        public FocusedFire Create()
        {
            foreach (var point in _spawnPoints)
            {
                if (!point.Spawned)
                    return Spawn(point);
            }

            return null;
        }

        private FocusedFire Spawn(SpawnPoint<FocusedFire> spawnPoint)
        {
            var instance = Instantiate(firePrefab, spawnPoint.Transform);
            
            spawnPoint.SetToThis(instance);
            
            instance.Movement
                .SetStartingPoint(spawnPoint.Transform)
                .SetSpeed(_ringsInstance.fireSpeed)
                .SetStaminaController(_ringsInstance.StaminaController);
            
            instance.SetDamage(_ringsInstance.CombatStats.Damage);

            return instance;
        }
    }
}