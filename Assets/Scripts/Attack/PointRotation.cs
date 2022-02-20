using UnityEngine;

public class PointRotation : MonoBehaviour
{
    public Vector3 mousePos;
    public Vector2 lookDir;
    public Transform Player;
    public Transform Cursor;
    public float angle;
    public float stabilizator;
    public static PointRotation instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void FixedUpdate()
    {
        transform.position = Player.position;
        
        if(!GetComponent<RatAttack>().is_Attack)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lookDir = mousePos - transform.position;
            angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - stabilizator;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
        else
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, GetComponent<RatAttack>().sp_rotation));
    }

    public void Flip()
    {
        base.transform.Rotate(0f,180f,0f);

    // if(stabilizator == -118f)
    //     stabilizator = -59f;
    
    // else if(stabilizator == -59f) 
    //     stabilizator = -118f;
    }
}
