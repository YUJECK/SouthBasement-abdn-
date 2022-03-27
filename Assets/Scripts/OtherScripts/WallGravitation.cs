using UnityEngine;

public class WallGravitation : MonoBehaviour
{
    public GameObject parent;
    public float gravityForDector = 1.3f;
    public float gravityForPlayer = 2f;
    public bool notWall;
    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.tag == "Decor")
        {
            coll.GetComponent<Rigidbody2D>().gravityScale = gravityForDector;
            if(notWall)
                parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        }
        else if(coll.tag == "Player")
        {
            coll.GetComponent<Rigidbody2D>().gravityScale = gravityForPlayer;
            if(notWall)
                parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        }
            
    }
    
    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Decor"||coll.tag == "Player")
        {
            coll.GetComponent<Rigidbody2D>().gravityScale = 0f;
            coll.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        }
    }
}
