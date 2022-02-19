using UnityEngine;

public class PointRotation : MonoBehaviour
{
    public Vector2 mousePos;
    public Vector2 lookDir;
    public Rigidbody2D rb;
    public Transform Player;
    public Camera cam;
    
    public float angle;
    public float stabilizator;
    public float UpCords;
    public float DownCords;
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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        transform.position = Player.position;
        
        if(!GetComponent<RatAttack>().is_Attack)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lookDir = mousePos - rb.position;
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - stabilizator;
            rb.rotation = angle;
        }
        else
            rb.rotation = GetComponent<RatAttack>().sp_rotation;
    }

    public void Flip()
    {
        transform.Rotate(0f,180f,0f);

        if(stabilizator == -118f)
            stabilizator = -59f;
        
        else if(stabilizator == -59f) 
            stabilizator = -118f;
    }
}
