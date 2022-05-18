using UnityEngine;

public class PointRotation : MonoBehaviour
{
    public bool stopRotating = false;
    public bool useMouse = true;
    [SerializeField] private Transform target;
    public bool playerTarget = true;
    private Camera mainCamera;
    private Vector2 mousePos;
    [SerializeField] private Transform center;
    [SerializeField] private Rigidbody2D rb;
    
    private void Start()
    {
        mainCamera = Camera.main;
        if(playerTarget) target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        transform.position = new Vector3(center.position.x, center.position.y, 0);
        if(!stopRotating) rb.rotation = CalculateAngle();
    }

    private float CalculateAngle()
    {
        Vector2 direction = new Vector2();
        
        if(useMouse)
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            direction = new Vector3(mousePos.x, mousePos.y, 0f) - center.position;
        }
        else if(target != null)
            direction = target.position - center.position;
        
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - 90f;
        return angle;
    }
}
