using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Characters;
using SouthBasement.Characters.Stats;
using SouthBasement.InternalAssets.InventorySystem.ItemBase;
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

        private int _currentHealthToReturn = 0;

        public StaminaController StaminaController { get; private set; }

        [Inject]
        private void Construct(Character character, StaminaController staminaController)
        {
            _character = character;
            StaminaController = staminaController;
        }

        public override void OnEquip()
        {
            _prefabInstance = Instantiate(prefab, _character.GameObject.transform);
            _prefabInstance.InitRingsInstance(this);
        }

        public override void OnTakeOff()
        {
            Destroy(_prefabInstance.gameObject);
        }

        public override Type GetItemType()
            => typeof(WeaponItem);

        public bool UseCulldown()
        {
            return true;
        }

        public AttackResult Attack()
        {
            if (_blocked) return new AttackResult(Array.Empty<Collider2D>());
            
            _prefabInstance.Create();

            Culldown(CombatStats.Multiplied.AttackRate);
            
            return new AttackResult(Array.Empty<Collider2D>());
        }

        private async void Culldown(float culldown)
        {
            _blocked = true;
            await UniTask.Delay(TimeSpan.FromSeconds(culldown ));
            _blocked = false;
        }
    }
}
