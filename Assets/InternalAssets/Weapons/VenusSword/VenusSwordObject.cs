using System;
using SouthBasement.Characters;
using SouthBasement.Scripts.Helpers;
using UnityEngine;

namespace SouthBasement
{
    public sealed class VenusSwordObject : MonoBehaviour
    {
        private TriggerCallback _attackTrigger;
        private StaminaController _staminaController;
        private VenusSword _venusSword;

        public float rotateSpeed = 1f;

        private void OnDestroy()
            => _attackTrigger.OnTriggerEnter -= Damage;

        public void Init(StaminaController staminaController, VenusSword venusSword)
        {
            if (_venusSword != null) 
                return;

            _attackTrigger = GetComponentInChildren<TriggerCallback>();
            _staminaController = staminaController;
            _venusSword = venusSword;
            
            _attackTrigger.transform.localPosition = new Vector3(0, _venusSword.AttackStatsConfig.AttackRange, 0f);
            _attackTrigger.OnTriggerEnter += Damage;
        }

        private void Damage(Collider2D obj)
        {
            if(obj.TryGetComponent(out IDamagable damagable) && _staminaController.CurrentStamina > 0)
                damagable.Damage(_venusSword.AttackStatsConfig.Damage, Array.Empty<string>());
        }

        private void FixedUpdate()
        {
            transform.Rotate(new Vector3(0, 0, 1f), -1f * rotateSpeed);

            if (rotateSpeed > 1f)
            {
                rotateSpeed -= 0.015f;
                _staminaController.TryDo(_venusSword.AttackStatsConfig.StaminaRequire * rotateSpeed);
            }
        }
    }
}