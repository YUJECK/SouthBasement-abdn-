using System;
using SouthBasement.Effects;
using SouthBasement.Characters;
using SouthBasement.Items;
using SouthBasement.Scripts.Helpers;
using UnityEngine;

namespace SouthBasement
{
    public sealed class VenusSwordObject : MonoBehaviour
    {
        private TriggerCallback _attackTrigger;
        private StaminaController _staminaController;
        private VenusSword _venusSword;
        private HitEffectSpawner _hitEffectSpawner;

        public float rotateSpeed = 1f;

        private void OnDestroy()
            => _attackTrigger.OnTriggerEnter -= Damage;

        public void Init(HitEffectSpawner hitEffectSpawner, StaminaController staminaController, VenusSword venusSword)
        {
            if (_venusSword != null) 
                return;

            _attackTrigger = GetComponentInChildren<TriggerCallback>();
            _staminaController = staminaController;
            _venusSword = venusSword;
            _hitEffectSpawner = hitEffectSpawner;
            
            _attackTrigger.transform.localPosition = new Vector3(0, _venusSword.CombatStats.AttackRange, 0f);
            _attackTrigger.OnTriggerEnter += Damage;
        }

        private void Damage(Collider2D obj)
        {
            if (obj.TryGetComponent(out IDamagable damagable) && _staminaController.CurrentStamina > 0)
            {
                damagable.Damage(_venusSword.CombatStats.Damage, Array.Empty<ItemsTags>());
                _hitEffectSpawner.Spawn(obj.transform.position);
            }
        }

        private void FixedUpdate()
        {
            transform.Rotate(new Vector3(0, 0, 1f), -1f * rotateSpeed);

            if (rotateSpeed > 1f)
            {
                rotateSpeed -= 0.015f;
                _staminaController.TryDo(_venusSword.CombatStats.StaminaRequire * rotateSpeed);
            }
        }
    }
}