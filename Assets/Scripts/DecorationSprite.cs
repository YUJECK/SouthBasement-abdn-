using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSprite : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            GetComponent<SpriteRenderer>().sortingOrder += 2;
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            GetComponent<SpriteRenderer>().sortingOrder -= 2;
    }
}
