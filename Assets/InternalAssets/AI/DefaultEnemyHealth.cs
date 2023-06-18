using SouthBasement.Economy;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace SouthBasement.AI
{
    [RequireComponent(typeof(Enemy))]
    public sealed class DefaultEnemyHealth : EnemyHealth
    {
        [SerializeField] private GameObject[] objectsToDrop;
        private CheeseService _cheeseService;

        [Inject]
        private void Construct(CheeseService cheeseService)
        {
            _cheeseService = cheeseService;
        }
        
        private void Awake()
        {
            Enemy = GetComponent<Enemy>();
            Enemy.OnDied += DropItems;
        }

        private void DropItems()
        {
            _cheeseService.SpawnCheese(transform.position, 4);
            
            foreach (var objectToDrop in objectsToDrop)
                Instantiate(objectToDrop, transform.position, transform.rotation);
        }
    }
}