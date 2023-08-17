using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Characters;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Weapon + "FocusRings")]
    public sealed class FocusRings : WeaponItem, IAttackOverridable
    {
        public FocusFireController prefab;
        public float fireSpeed;
        
        private Character _character;

        private FocusFireController _prefabInstance;
        private bool _blocked;

        [Inject]
        private void Construct(Character character)
        {
             _character = character;
        }

        public override void OnEquip()
        {
            _prefabInstance = Instantiate(prefab, _character.GameObject.transform);
            _prefabInstance.InitRingsInstance(this);
        }

        public override void OnRemoved()
        {
            Destroy(_prefabInstance.gameObject);
        }

        public override Type GetItemType()
            => typeof(WeaponItem);

        public IDamagable[] Attack()
        {
            if (_blocked) return Array.Empty<IDamagable>();
            
            _prefabInstance.Create();

            Culldown(AttackStatsConfig.AttackRate);
            
            return Array.Empty<IDamagable>();
        }

        public async void Culldown(float culldown)
        {
            _blocked = true;
            await UniTask.Delay(TimeSpan.FromSeconds(culldown ));
            _blocked = false;
        }
    }
}
