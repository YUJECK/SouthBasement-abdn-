using System.Collections;
using UnityEngine;

namespace TheRat.InternalAssets.Scripts
{
    [RequireComponent(typeof(IDamagable))]
    public sealed class DamagableVisualization : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color damageColor;
        [SerializeField] private string playTrigger;
        [SerializeField] private Animator animator;

        private Coroutine _coroutine;
        
        private void Awake()
        {
            GetComponent<IDamagable>().OnDamaged += OnDamaged;
        }

        private void OnDamaged(int damage)
        {
            if(_coroutine != null)
                StopCoroutine(_coroutine);
            
            if(playTrigger != "" && animator != null)
                animator.SetTrigger(playTrigger);
                
            _coroutine = StartCoroutine(GetRed());
        }

        private IEnumerator GetRed()
        {
            _spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(0.4f);
            _spriteRenderer.color = Color.white;
        }
    }
}