using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Characters;
using SouthBasement.Characters.Stats;
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
        private CharacterHealthStats _healthStats;

        private int _currentHealthToReturn = 0;

        public StaminaController StaminaController { get; private set; }

        [Inject]
        private void Construct(Character character, CharacterHealthStats healthStats, StaminaController staminaController)
        {
            _character = character;
            _healthStats = healthStats;
            StaminaController = staminaController;
        }

        public override void OnEquip()
        {
            _prefabInstance = Instantiate(prefab, _character.GameObject.transform);
            _prefabInstance.InitRingsInstance(this);
        }

        public override void OnUnequip()
        {
            Destroy(_prefabInstance.gameObject);
        }

        public override Type GetItemType()
            => typeof(WeaponItem);

        public bool UseCulldown()
        {
            return true;
        }

        public IDamagable[] Attack()
        {
            if (_blocked) return Array.Empty<IDamagable>();
            
            _prefabInstance.Create();

            Culldown(CombatStats.AttackRate);
            
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
