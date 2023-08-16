using UnityEngine;

namespace SouthBasement
{
    public class FocusedFire : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.TryGetComponent(out IDamagable damagable))
                damagable.Damage(10, new[]{"magic"});
            
            Destroy(gameObject);
        }
    }
}
