using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(FocusedFireTargetChecker))]
    [RequireComponent(typeof(FocusedFireMovement))]
    public class FocusedFire : MonoBehaviour
    {
        public FocusedFireMovement Movement { get; private set; }
        public FocusedFireTargetChecker Checker { get; private set; }

        private int _damage;

        private void Awake()
        {
            Checker = GetComponent<FocusedFireTargetChecker>();
            Movement = GetComponent<FocusedFireMovement>();
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
            
            Destroy(gameObject);
        }
    }
}
