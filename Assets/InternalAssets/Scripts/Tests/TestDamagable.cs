using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TheRat.Tests
{
    public sealed class TestDamagable : MonoBehaviour, IDamagable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public int CurrentHealth { get; private set; } = 35;
        
        public async void Damage(int damage)
        {
            _spriteRenderer.color = Color.red;
            await UniTask.Delay(500);
            _spriteRenderer.color = Color.white;
            
            CurrentHealth -= damage;
            if(CurrentHealth <= 0)
                Destroy(gameObject);
        }
    }
}