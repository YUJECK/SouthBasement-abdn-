using TheRat.AI;
using TheRat.Player;
using UnityEngine;

namespace TheRat.Generation
{
    public sealed class EnemyController : MonoBehaviour
    {
        private Enemy[] _enemies;

        private void Awake()
        {
            _enemies = GetComponentsInChildren<Enemy>();
            GetComponent<PlayerEnterTrigger>().OnEntered += OnEntered;
        }

        private void OnEntered(Character obj)
        {
            foreach (var enemy in _enemies)
                enemy.Enable();
        }
    }
}