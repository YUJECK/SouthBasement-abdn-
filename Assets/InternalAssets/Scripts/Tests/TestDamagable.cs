using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TheRat.Tests
{
    public sealed class TestDamagable : MonoBehaviour, IDamagable
    {
        public int CurrentHealth { get; }
        
        public async void Damage(int damage)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            await UniTask.Delay(500);
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
}