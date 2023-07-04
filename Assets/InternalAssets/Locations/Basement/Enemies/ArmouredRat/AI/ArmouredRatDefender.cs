using SouthBasement.Generation;
using UnityEngine;

namespace SouthBasement.Basement.Enemies.ArmouredRat.AI
{
    [RequireComponent(typeof(PlayerEnterTrigger))]
    public sealed class ArmouredRatDefender : MonoBehaviour
    {
        private ArmouredRatHealth _armouredRatHealth;
        private ArmouredRatAnimator _armouredRatAnimator;
        private PlayerEnterTrigger _playerEnterTrigger;

        public bool CurrentDefending => _armouredRatHealth.CurrentDefends;

        private void Start()
        {   
            _armouredRatHealth = GetComponentInParent<ArmouredRatHealth>();
            _armouredRatAnimator = GetComponentInParent<ArmouredRatAI>().EnemyAnimator;
            _playerEnterTrigger = GetComponent<PlayerEnterTrigger>();
        }
    
        public bool CanDefends()
            => _playerEnterTrigger.CurrentOnTrigger;

        public void Defend()
        {
            _armouredRatHealth.CurrentDefends = true;
            _armouredRatAnimator.PlayDefends();
        }
        public void UnDefend()
        {
            _armouredRatHealth.CurrentDefends = false;
            _armouredRatAnimator.PlayIdle();
        }
    }
}