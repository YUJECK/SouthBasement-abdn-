using SouthBasement.AI;
using UnityEngine;

namespace SouthBasement.Basement.Enemies.ArmouredRat.AI
{
    public sealed class ArmouredRatHealth : DefaultEnemyHealth
    {
        [SerializeField] private AudioSource _shieldHitSound;
        public bool CurrentDefends { get; set; }

        public override void Damage(int damage, string[] args)
        {
            if(!CurrentDefends)
                base.Damage(damage, args);
            else _shieldHitSound.Play();
        }
    }
}