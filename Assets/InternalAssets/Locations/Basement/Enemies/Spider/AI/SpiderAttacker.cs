using UnityEngine;

namespace TheRat
{
    public sealed class SpiderAttacker : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D BulletPrefab { get; private set; }

        public void ThrowYarn()
        {
            var bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            bullet.AddForce(bullet.transform.up * 10, ForceMode2D.Impulse);
        }
        public void ThrowYarn(Rigidbody2D spawnedProjectile)
        {
            spawnedProjectile.transform.rotation = transform.rotation;
            spawnedProjectile.AddForce(spawnedProjectile.transform.up * 10, ForceMode2D.Impulse);
        }
    }
}