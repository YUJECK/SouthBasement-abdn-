using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSprite : MonoBehaviour
{
    [SerializeField] private int layerPlus = 2;
    [SerializeField] private int defoultLayer = 2;
    [SerializeField] private bool changeLayer;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "PLayer" && changeLayer && coll.GetComponent<SpriteRenderer>()!=null)
        {
            defoultLayer = GetComponent<SpriteRenderer>().sortingOrder;
            GetComponent<SpriteRenderer>().sortingOrder = coll.GetComponent<SpriteRenderer>().sortingOrder + layerPlus;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Plaeyr" && changeLayer&& coll.GetComponent<SpriteRenderer>()!=null)
            GetComponent<SpriteRenderer>().sortingOrder = defoultLayer;
    }
}
