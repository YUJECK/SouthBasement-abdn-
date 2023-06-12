using System.Collections;
using UnityEngine;

namespace TheRat.InternalAssets.Scripts
{
    [RequireComponent(typeof(IDamagable))]
    public sealed class DamagableVisualization : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Coroutine _coroutine;
        
        private void Awake()
        {
            GetComponent<IDamagable>().OnDamaged += OnDamaged;
        }

        private void OnDamaged(int obj)
        {
            if(_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(GetRed());
        }

        private IEnumerator GetRed()
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.4f);
            _spriteRenderer.color = Color.white;
        }
    }
}