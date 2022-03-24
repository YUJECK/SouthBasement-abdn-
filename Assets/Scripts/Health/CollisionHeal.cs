using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHeal : MonoBehaviour
{
    public int collisionHeal = 1;
    public string collisionTag;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == collisionTag)
        {
            Health health = coll.gameObject.GetComponent<Health>();
            
            if(health.health != health.maxHealth)
            {
                health.Heal(collisionHeal);
                Destroy(gameObject);
            }
        }
    }
}
