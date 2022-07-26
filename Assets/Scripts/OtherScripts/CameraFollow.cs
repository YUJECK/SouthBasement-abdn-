using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float standartValaue = 6f;
    public Transform target;
    private Transform player;
    public static CameraFollow instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        player = FindObjectOfType<PlayerController>().transform;
        target = player;
    }
    
    private void OnLevelWasLoaded(int level)
    {
        GetComponent<Camera>().orthographicSize = standartValaue;
    }

    void Update()
    {
        if (target != null)
        {
            if (new Vector3(transform.position.x, transform.position.y, 0f) != new Vector3(target.position.x, target.position.y, 0f))
                transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
        else if(player != null) SetTarget(player);
    }

    public void SetTarget(Transform newTarget) => target = newTarget;
    public void ResetTarget() => target = player;
}
