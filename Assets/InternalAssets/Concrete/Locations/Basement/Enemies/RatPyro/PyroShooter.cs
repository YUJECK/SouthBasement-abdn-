using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement
{
    public sealed class PyroShooter : MonoBehaviour
    {
        public List<Projectile> petards;
        public Transform spawnPoint;
        
        public void Shoot()
        {
            var petard = GetRandomPetard();

            SpawnAndForce(petard);
        }

        private void SpawnAndForce(Projectile petard)
        {
            var instance = Instantiate(petard, spawnPoint.position, spawnPoint.rotation);
            
            instance.Rigidbody.AddForce(transform.up * 20);
        }

        private Projectile GetRandomPetard()
        {
            throw new System.NotImplementedException();
        }
    }
}