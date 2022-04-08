using UnityEngine;

public class PointRotation : MonoBehaviour
{
    public bool stopRotating = false;
    private float angle;
    private Camera mainCamera;
    private Vector2 mousePos;
    [SerializeField] private Transform playerCenter;
    [SerializeField] private Rigidbody2D rb;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.position = playerCenter.position;
        if(!stopRotating) rb.rotation = CalculateAngle();
    }

    private float CalculateAngle()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector3(mousePos.x, mousePos.y, 0f) - playerCenter.position;
        angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - 90f;
        return angle;
    }
}
