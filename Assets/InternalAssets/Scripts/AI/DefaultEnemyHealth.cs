using System;
using UnityEngine;

namespace TheRat.AI
{
    [RequireComponent(typeof(Enemy))]
    public sealed class DefaultEnemyHealth : EnemyHealth
    {
        private void Awake()
        {
            Enemy = GetComponent<Enemy>();
        }
    }
}