using UnityEngine;
using System.Collections;

public enum PointRotationTargetType
{
    Mouse,
    Other
}
public class PointRotation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PointRotationTargetType targetType;
    [SerializeField] private bool usePlayerAsTarget;
    [SerializeField] private Transform target;

    [Header("Info")]
    public float offset = 0f;
    public float coefficient = 1f;

    //private variables
    private bool stopRotating = false; 
    private Vector3 lastTargetPosition;
    [SerializeField] private float angle; //current angle

    private Camera mainCamera;

    //getters
    public bool UsePlayerAsTarget => usePlayerAsTarget;
    public Transform Target => target;
    public PointRotationTargetType TargetType => targetType;

    //setters
    public void SetTarget(Transform newTarget)
    {
        if (targetType == PointRotationTargetType.Other) target = newTarget;
        else Debug.LogWarning("Rotation type is - " + targetType);
    }
    public void StopRotating(bool active, float time) { StartCoroutine(StopRotatingCoroutine(active, time)); }

    private IEnumerator StopRotatingCoroutine(bool active, float time)
    {
        stopRotating = active;
        yield return new WaitForSeconds(time);
        stopRotating = !active;
    }

    private float CalculateAngle()
    {
        if (!stopRotating)
        {
            Vector3 targetPosition = DefineTargetPosition();
            Vector2 direction = CalculateDirection(targetPosition);

            lastTargetPosition = targetPosition;

            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            return coefficient * angle + offset;
        }
        return angle;
    }
    private Vector3 DefineTargetPosition()
    {
        if (targetType == PointRotationTargetType.Other) return target.position;
        else return new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y, 0f);
    }
    private Vector2 CalculateDirection(Vector3 targetPosition) => transform.position - targetPosition;

    //unity methods
    private void Awake() => mainCamera = Camera.main;
    private void FixedUpdate()
    {
        //�������� 
        if (DefineTargetPosition() != lastTargetPosition)
            transform.rotation = Quaternion.Euler(0f, 0f, CalculateAngle());
    }
}