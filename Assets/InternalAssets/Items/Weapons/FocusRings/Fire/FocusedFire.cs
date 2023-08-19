using System.Collections;
using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(FocusedFireTargetChecker))]
    [RequireComponent(typeof(FocusedFireMovement))]
    [RequireComponent(typeof(Animator))]
    public class FocusedFire : MonoBehaviour
    {
        [SerializeField] private AudioSource explodeSound;
        
        public FocusedFireMovement Movement { get; private set; }
        public FocusedFireTargetChecker Checker { get; private set; }
        
        private Animator _animator;
        private readonly int _explodeAnimation = Animator.StringToHash("FireExplode");
        private int _damage;

        private void Awake()
        {
            Checker = GetComponent<FocusedFireTargetChecker>();
            Movement = GetComponent<FocusedFireMovement>();
            
            _animator = GetComponent<Animator>();
        }

        public void SetDamage(int damage)
        {
            if(damage > 0)
                _damage = damage;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.TryGetComponent(out IDamagable damagable))
                damagable.Damage(_damage, new[]{"magic"});

            StartCoroutine(Explode());
        }

        private IEnumerator Explode()
        {
            Destroy(GetComponent<Collider2D>());
            _animator.Play(_explodeAnimation);
            
            yield return new WaitForSeconds(0.15f);
           
            explodeSound.Play();
            Destroy(gameObject);
        }
    }
}
