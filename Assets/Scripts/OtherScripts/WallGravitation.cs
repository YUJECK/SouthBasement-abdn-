using UnityEngine;

public class WallGravitation : MonoBehaviour
{
    public GameObject parent;
    public bool notWall;
    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.tag == "Decor")
        {
            coll.GetComponent<Rigidbody2D>().gravityScale = 1.3f;
            if(notWall)
                parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        }
    }
    
    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Decor")
        {
            coll.GetComponent<Rigidbody2D>().gravityScale = 0f;
            coll.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        }
    }
}
