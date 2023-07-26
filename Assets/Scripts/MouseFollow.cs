using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
}