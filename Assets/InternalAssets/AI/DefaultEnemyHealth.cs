using SouthBasement.Economy;
using UnityEngine;
using Zenject;

namespace SouthBasement.AI
{
    [RequireComponent(typeof(Enemy))]
    public sealed class DefaultEnemyHealth : EnemyHealth
    {
        [SerializeField] private GameObject[] objectsToDrop;
        [SerializeField] private AudioSource hitSound;
        [SerializeField] private int cheeseAmount;
        
        private CheeseService _cheeseService;

        [Inject]
        private void Construct(CheeseService cheeseService)
            => _cheeseService = cheeseService;

        private void Awake()
        {
            Enemy = GetComponent<Enemy>();
            
            Enemy.OnDied += DropItems;
            OnDamaged += PlayHitSound;
        }

        private void PlayHitSound(int obj)
        {
            if (hitSound != null) 
                hitSound.Play();
        }

        private void DropItems(Enemy enemy)
        {
            _cheeseService.SpawnCheese(transform.position, cheeseAmount);
            
            foreach (var objectToDrop in objectsToDrop)
                Instantiate(objectToDrop, transform.position, transform.rotation);
        }
    }
}