using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AutumnForest.Tests
{
    public sealed class TestDamagable : MonoBehaviour, IDamagable
    {
        public int CurrentHealth { get; }
        
        public async void Damage(int damage)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            await UniTask.Delay(500);
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}