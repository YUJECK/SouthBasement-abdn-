using System;
using System.Collections;
using SouthBasement.Weapons;
using UnityEngine;

namespace SouthBasement
{
    public class PumpBoom : MonoBehaviour
    {
        private CombatStats _combatStats;
        public GameObject boomEffect;

        public float reange;
        
        public void Set(CombatStats combatStats)
        {
            _combatStats = combatStats;
        }
        
        void Start()
        {
            StartCoroutine(Boom());
        }

        private IEnumerator Boom()
        {
            yield return new WaitForSeconds(_combatStats.Multiplied.AttackRate);

            var result = Physics2D.OverlapCircleAll(transform.position, _combatStats.AttackRange);

            foreach (var obj in result)
            {
                if(obj.TryGetComponent(out IDamagable damagable))
                    damagable.Damage(_combatStats.Multiplied.Damage, _combatStats.AttackTags.ToArray());
            }

            Instantiate(boomEffect, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, reange);
        }
    }
}
