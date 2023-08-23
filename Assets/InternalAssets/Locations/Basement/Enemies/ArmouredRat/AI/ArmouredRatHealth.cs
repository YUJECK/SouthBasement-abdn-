using System.Linq;
using SouthBasement.AI;
using SouthBasement.Items;
using UnityEngine;

namespace SouthBasement.Basement.Enemies.ArmouredRat.AI
{
    public sealed class ArmouredRatHealth : DefaultEnemyHealth
    {
        [SerializeField] private AudioSource _shieldHitSound;
        
        private bool _currentDefends;

        public bool CurrentDefends
        {
            get => _currentDefends;
            set
            {
                _currentDefends = value;
                EffectsHandler.Blocked = value;
            }
        }

        public override void Damage(int damage, ItemsTags[] args)
        {
            if(!CurrentDefends || args.Contains(ItemsTags.Effect)) base.Damage(damage, args);
            else _shieldHitSound.Play();
        }
    }
}