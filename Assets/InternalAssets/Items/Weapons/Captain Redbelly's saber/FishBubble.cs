using System.Collections;
using SouthBasement.Items;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace SouthBasement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class FishBubble : MonoBehaviour
    {
        [SerializeField] private float range;
        [SerializeField] private AudioSource popSound;
        [SerializeField] private GameObject fishPrefab;
        
        private int _damage = 3;
        private ItemsTags[] _args = new [] {ItemsTags.Water};
        private readonly int _explosionAnimation = Animator.StringToHash("FishBubbleExplode");

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private readonly Collider2D[] _results = new Collider2D[8];

        public void SetDamage(int damage)
        {
            if(damage < 0) 
                return;
            
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
                _rigidbody.velocity = new Vector2(Random.Range(-4f, 4f), Random.Range(-4f, 4f));
                yield return new WaitForSeconds(2f);
            }

            StartCoroutine(Exlposion());
        }

        private IEnumerator Exlposion()
        {
            _animator.Play(_explosionAnimation);
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, range, _results);

            for (int i = 0; i < size; i++)
            {
                if(_results[i].gameObject.TryGetComponent(out IDamagable damagable))
                    damagable.Damage(_damage, _args);
            }

            popSound.Play();
            yield return new WaitForSeconds(0.4f);

            DropFish();
            
            
            Destroy(gameObject);
        }

        private void DropFish()
        {
            int willDrop = UnityEngine.Random.Range(0, 2);

            if (willDrop == 0) Instantiate(fishPrefab, transform.position, Quaternion.identity);
        }

        private void OnDrawGizmos()
            => Gizmos.DrawWireSphere(transform.position, range);
    }
}
