using UnityEngine;

public class PointRotation : MonoBehaviour
{
    public enum TargetType
    {
        Player,
        Mouse,
        Other
    }

    public TargetType targetType;
    [SerializeField] private Transform target;
    private Camera mainCamera;
    private Vector2 mousePos;
    [SerializeField] private bool movePosToCenter;
    [SerializeField] private Transform center;
    [SerializeField] private Rigidbody2D rb;
    private bool stopRotating = false;
    
    private void Start()
    {
        mainCamera = Camera.main;
        if(targetType == TargetType.Player) target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        Debug.Log(stopRotating);
        if(center != null && movePosToCenter) transform.position = new Vector3(center.position.x, center.position.y, 0);
        //if(!stopRotating) rb.rotation = CalculateAngle();
        transform.localRotation = Quaternion.Euler(0f, CalculateAngle(), 0f);
    }

    private float CalculateAngle()
    {
        Vector2 direction = new Vector2();
        
        if(targetType == TargetType.Mouse)
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            direction = new Vector3(mousePos.x, mousePos.y, 0f) - center.position;
        }
        else if(target != null)
            direction = target.position - center.position;
        
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - 90f;
        Debug.Log(angle);
        return angle;
    }
    public void StopRotating(bool active) { stopRotating = !active; }
}
