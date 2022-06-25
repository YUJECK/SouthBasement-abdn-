using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHeal : MonoBehaviour
{
    public int collisionHeal = 10;
    public string collisionTag;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == collisionTag)
        {
            PlayerHealth health = coll.gameObject.GetComponent<PlayerHealth>();
            
            if(health.health != health.maxHealth)
            {
                health.Heal(collisionHeal);
                Destroy(gameObject);
            }
        }
    }
}
