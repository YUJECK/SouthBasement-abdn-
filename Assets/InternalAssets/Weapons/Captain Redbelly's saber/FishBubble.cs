using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SouthBasement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class FishBubble : MonoBehaviour
    {
        [SerializeField] private float range;
        [SerializeField] private AudioSource _popSound;
        
        private int _damage = 3;
        private string[] args;
        private readonly int ExplosionAnimation = Animator.StringToHash("FishBubbleExplode");

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private readonly Collider2D[] _results = new Collider2D[8];

        public void SetDamage(int damage)
        {
            if(damage < 0) return;
            
            _damage = damage;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            
            StartCoroutine(Movement());
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.TryGetComponent(out IDamagable damagable))
                StartCoroutine(Exlposion());
        }

        private IEnumerator Movement()
        {
            float startTime = Time.time;
            const float duration = 9f;

            while (Time.time < startTime + duration)
            {
                _rigidbody.velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
                yield return new WaitForSeconds(2f);
            }

            StartCoroutine(Exlposion());
        }

        private IEnumerator Exlposion()
        {
            _animator.Play(ExplosionAnimation);
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, range, _results);

            for (int i = 0; i < size; i++)
            {
                if(_results[i].gameObject.TryGetComponent(out IDamagable damagable))
                    damagable.Damage(_damage, args);
            }

            _popSound.Play();
            yield return new WaitForSeconds(0.4f);
            
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
            => Gizmos.DrawWireSphere(transform.position, range);
    }
}
