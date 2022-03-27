using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSprite : MonoBehaviour
{
    [SerializeField] private int layerPlus = 2;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            GetComponent<SpriteRenderer>().sortingOrder += layerPlus;
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            GetComponent<SpriteRenderer>().sortingOrder -= layerPlus;
    }
}
