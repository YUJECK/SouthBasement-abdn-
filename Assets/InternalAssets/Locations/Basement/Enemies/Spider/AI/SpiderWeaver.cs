using System.Collections;
using UnityEngine;

namespace SouthBasement
{
    public sealed class SpiderWeaver : MonoBehaviour
    {
        [field: SerializeField] public Projectile BulletPrefab { get; private set; }

        public Coroutine Weave() => StartCoroutine(WeaveRoutine());

        private IEnumerator WeaveRoutine()
        {
            var projectile = InstatiateProjectile();
            projectile.DisableCollider();
            
            yield return WeaveProjectile(projectile.transform, 2f);
            
            projectile.EnableCollider();
            LaunchProjectile(projectile);
        }

        private IEnumerator WeaveProjectile(Transform projectile, float weaveTime)
        {
            projectile.localScale = Vector3.zero;
            
            var weavePlus = new Vector3(0.1f, 0.1f, 0f);
            float rate = weaveTime / 10;

            while (projectile.localScale is {x: < 1f, y: < 1f})
            {
                projectile.localScale += weavePlus;
                yield return new WaitForSeconds(rate);
            }
        }

        private Projectile InstatiateProjectile()
        {
            Projectile projectile 
                = Instantiate(BulletPrefab, transform.position, transform.rotation, transform.parent);
            
            return projectile;
        }

        public void LaunchProjectile(Projectile spawnedProjectile)
        {
            spawnedProjectile.transform.rotation = transform.rotation;
            spawnedProjectile.LaunchProjectile(10f);
        }
    }
}