using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSprite : MonoBehaviour
{
    [SerializeField] private int layerPlus = 2;
    [SerializeField] private bool changeLayer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private bool changeCollider;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(changeLayer && coll.GetComponent<SpriteRenderer>()!=null)
            GetComponent<SpriteRenderer>().sortingOrder = coll.GetComponent<SpriteRenderer>().sortingOrder + layerPlus;
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(changeLayer&& coll.GetComponent<SpriteRenderer>()!=null)
            GetComponent<SpriteRenderer>().sortingOrder = coll.GetComponent<SpriteRenderer>().sortingOrder - layerPlus;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(changeCollider)
        {
            if(coll.gameObject.tag != "PlayerLegs")
                _collider.enabled = false;
            else if(coll.gameObject.tag == "PlayerLegs")
                _collider.enabled = true;
        }
    }
}
