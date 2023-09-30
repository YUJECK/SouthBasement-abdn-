using SouthBasement.Economy;
using SouthBasement.Extensions;
using UnityEngine;
using Zenject;

namespace SouthBasement.AI
{
    public class DefaultEnemyHealth : EnemyHealth
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
            Enemy = gameObject.Get<Enemy>();
            EffectsHandler = gameObject.Get<EffectsHandler>();
            
            Enemy.OnDied += DropItems;
            
            Enemy.OnEnable += OnEnemyEnable;
            Enemy.OnDisable += OnEnemyDisable;
            
            OnDamaged += PlayHitSound;
        }

        private void OnEnemyDisable(Enemy obj)
        {
            DefendedFromHits = true;
        }

        private void OnEnemyEnable(Enemy obj)
        {
            DefendedFromHits = false;
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