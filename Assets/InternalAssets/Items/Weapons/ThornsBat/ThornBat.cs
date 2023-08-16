using System;
using SouthBasement.Characters;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Weapon + "ThornBat")]
    public class ThornBat : WeaponItem
    {
        [SerializeField] private Thorn _thornPrefab;
        private Character _character;
        
        [Inject]
        private void Construct(Character character) 
            => _character = character;

        public override Type GetItemType() => typeof(WeaponItem);

        public override void OnAttack(IDamagable[] damagables)
        {
            int thornsCount = GetThornsCount();
            
            for (int i = 0; i < thornsCount; i++)
                Instantiate(_thornPrefab, GetPosition(), Quaternion.identity);
        }

        private int GetThornsCount()
        {
            return UnityEngine.Random.Range(1, 3);
        }
        
        private Vector3 GetPosition()
        {
            return new Vector3(_character.GameObject.transform.position.x + UnityEngine.Random.Range(-0.7f, 0.7f),
            _character.GameObject.transform.position.y + UnityEngine.Random.Range(-0.7f, 0.7f),
                0f);
        }
    }
}
