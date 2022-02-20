using UnityEngine;

public class PointRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 mousePos;

    [SerializeField]
    private Vector2 lookDir;

    [SerializeField]
    private Transform Player;

    [SerializeField]
    private Transform Cursor;

    [SerializeField]
    private float angle;

    [SerializeField]
    private float stabilizator;
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
        transform.Rotate(0f,180f,0f);
    }
}
