using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Characters;
using SouthBasement.Characters.Components;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [CreateAssetMenu]
    public sealed class VenusSword : WeaponItem, IAttackOverridable
    {
        private const float RotateSpeed = 3f;
        [SerializeField] private VenusSwordObject venusSwordPrefab;
        
        private VenusSwordObject _currentVenusSword;
        private Character _character;
        private StaminaController _staminaController;

        [Inject]
        private void Construct(StaminaController staminaController, Character character)
        {
            _staminaController = staminaController;
            _character = character;
        }

        public override Type GetItemType()
            => typeof(WeaponItem);

        public IDamagable[] Attack()
        {
            if(_currentVenusSword.rotateSpeed < 10)
                _currentVenusSword.rotateSpeed += RotateSpeed;
            
            Culldown(AttackStatsConfig.AttackRate);
        
            return null;
        }

        private async void Culldown(float culldown)
        {
            _character.Components.Get<ICharacterAttacker>().Blocked = true;
            await UniTask.Delay(TimeSpan.FromSeconds(culldown));
            _character.Components.Get<ICharacterAttacker>().Blocked = false;
        }

        public override void OnEquip()
        {
            if (_currentVenusSword == null)
            {
                _currentVenusSword = Instantiate(venusSwordPrefab, _character.GameObject.transform.position, Quaternion.identity, _character.GameObject.transform);
                _currentVenusSword.Init(_staminaController, this);
            }
        }

        public override void OnRemoved()
            => Destroy(_currentVenusSword.gameObject);
    }
}
