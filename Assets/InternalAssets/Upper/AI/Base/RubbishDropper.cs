using System.Collections.Generic;
using SouthBasement.AI;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.Items;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [RequireComponent(typeof(Enemy))]
    public class RubbishDropper : MonoBehaviour
    {
        public List<SpawnObject<RubbishItem>> Rubbish;
        private Enemy _enemyHealth;
        private ItemsContainer _itemsContainer;

        [Inject]
        private void Construct(ItemsContainer itemsContainer)
        {
            _itemsContainer = itemsContainer;
        }
        
        private void Start()
        {
            _enemyHealth = GetComponent<Enemy>();
            
            _enemyHealth.OnDied += OnDied;
        }

        private void OnDied(Enemy obj)
        {
            float chance = Random.Range(0, 100);
            int itemsCount = Random.Range(1, Rubbish.Count);

            for(int i = 0; i < itemsCount; i++)
            {
                if (Rubbish[i].SpawnChance >= chance)
                    _itemsContainer.SpawnItem(Rubbish[i].Prefab, transform.position);
            }
        }
    }
}
